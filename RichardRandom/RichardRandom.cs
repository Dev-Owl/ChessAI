using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABChess.Engine;

namespace AI
{
    public class RichardRandom : IAI
    {
        public MoveGenerator Engine { get; set; }
        private Random rnd;
        private int MyColor;
        System.Threading.Timer timer;

        public RichardRandom()
        { 
        
        }

        #region IAI Members

        public void Init(MoveGenerator EngineMoveGenerator,int PlayerColor)
        {
            this.Engine = EngineMoveGenerator;
            rnd = new Random();
            MyColor = PlayerColor;
            EngineMoveGenerator.AfterMove += EngineMoveGenerator_AfterMove;
            EngineMoveGenerator.BeforeMove += EngineMoveGenerator_BeforeMove;
            EngineMoveGenerator.GameEnded += EngineMoveGenerator_GameEnded;
        }
        #endregion

        void EngineMoveGenerator_GameEnded(object sender, GameEndedEventArgs e)
        {
            MatchEnding();
        }

        void EngineMoveGenerator_BeforeMove(object sender, FigureMoveEventArgs e)
        {
            //Nothing to do here ...
        }

        void EngineMoveGenerator_AfterMove(object sender, FigureMoveEventArgs e)
        {
            if (e.MovedFigure.Color != this.MyColor)
            {
                RaiseAfterMoveTimeout(1000);
            }
        }

        private void RaiseAfterMoveTimeout(int DelayTime)
        {
            if( timer == null)
            {
                timer = new System.Threading.Timer(delegate { this.YourTurn(); }, null, DelayTime, System.Threading.Timeout.Infinite);
            }
            else
            {
                timer.Change(DelayTime, System.Threading.Timeout.Infinite);
            }
            

        }

        private void YourTurn()
        {
            bool moved = false;
            int currentSquare = 0;
            while (!moved)
            {
                currentSquare=rnd.Next(0, 64);
                Figure figure = this.Engine.GetFigureAtPosition((ulong)Math.Pow(2, currentSquare),Engine.CurrentGameState);
                if (figure != null && figure.Color == this.MyColor)
                {
                    UInt64 figureMoves = Engine.GetMoveForFigure(figure, (short)currentSquare, Engine.CurrentGameState);
                    if (figureMoves != 0)
                    {
                        Engine.MakeAMove(figure, (short)ABChess.Tools.BitOperations.MostSigExponent(figureMoves),Engine.CurrentGameState);
                        moved = true;
                    }
                }
            }
        }

        private void MatchEnding()
        {
            this.Engine = null;
            this.rnd = null;
        }

      

        #region IPromotion Members

        public EFigures GetDecision()
        {
            return EFigures.Queen;
        }

        #endregion
    }
}
