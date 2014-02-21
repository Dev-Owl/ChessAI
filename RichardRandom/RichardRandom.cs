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
        public RichardRandom()
        { 
        
        }

        #region IAI Members

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void YourTurn()
        {
            throw new NotImplementedException();
        }

        public void ForceMove()
        {
            throw new NotImplementedException();
        }

        public void MatchEnding()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPromotion Members

        public EFigures GetDecision()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
