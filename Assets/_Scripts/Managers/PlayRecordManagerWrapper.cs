using System;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    [Serializable]
    public class PlayRecordManagerWrapper
    {
        [SerializeField] private int numberOfPlaysForBankkreditNovel;
        [SerializeField] private int numberOfPlaysForBekannteTreffenNovel;
        [SerializeField] private int numberOfPlaysForBankkontoNovel;
        [SerializeField] private int numberOfPlaysForFoerderantragNovel;
        [SerializeField] private int numberOfPlaysForElternNovel;
        [SerializeField] private int numberOfPlaysForNotarinNovel;
        [SerializeField] private int numberOfPlaysForPresseNovel;
        [SerializeField] private int numberOfPlaysForBueroNovel;
        [SerializeField] private int numberOfPlaysForGruendungszuschussNovel;
        [SerializeField] private int numberOfPlaysForHonorarNovel;
        [SerializeField] private int numberOfPlaysForLebenspartnerNovel;
        [SerializeField] private int numberOfPlaysForIntroNovel;

        public void SetNumberOfPlaysForBankkreditNovel(int numberOfPlaysForBankkreditNovel)
        {
            this.numberOfPlaysForBankkreditNovel = numberOfPlaysForBankkreditNovel;
        }

        public int GetNumberOfPlaysForBankkreditNovel()
        {
            return numberOfPlaysForBankkreditNovel;
        }

        public void SetNumberOfPlaysForBekannteTreffenNovel(int numberOfPlaysForBekannteTreffenNovel)
        {
            this.numberOfPlaysForBekannteTreffenNovel = numberOfPlaysForBekannteTreffenNovel;
        }

        public int GetNumberOfPlaysForBekannteTreffenNovel()
        {
            return numberOfPlaysForBekannteTreffenNovel;
        }

        public void SetNumberOfPlaysForBankkontoNovel(int numberOfPlaysForBankkontoNovel)
        {
            this.numberOfPlaysForBankkontoNovel = numberOfPlaysForBankkontoNovel;
        }

        public int GetNumberOfPlaysForBankkontoNovel()
        {
            return numberOfPlaysForBankkontoNovel;
        }

        public void SetNumberOfPlaysForFoerderantragNovel(int numberOfPlaysForFoerderantragNovel)
        {
            this.numberOfPlaysForFoerderantragNovel = numberOfPlaysForFoerderantragNovel;
        }

        public int GetNumberOfPlaysForFoerderantragNovel()
        {
            return numberOfPlaysForFoerderantragNovel;
        }

        public void SetNumberOfPlaysForElternNovel(int numberOfPlaysForElternNovel)
        {
            this.numberOfPlaysForElternNovel = numberOfPlaysForElternNovel;
        }

        public int GetNumberOfPlaysForElternNovel()
        {
            return numberOfPlaysForElternNovel;
        }

        public void SetNumberOfPlaysForNotarinNovel(int numberOfPlaysForNotarinNovel)
        {
            this.numberOfPlaysForNotarinNovel = numberOfPlaysForNotarinNovel;
        }

        public int GetNumberOfPlaysForNotarinNovel()
        {
            return numberOfPlaysForNotarinNovel;
        }

        public void SetNumberOfPlaysForPresseNovel(int numberOfPlaysForPresseNovel)
        {
            this.numberOfPlaysForPresseNovel = numberOfPlaysForPresseNovel;
        }

        public int GetNumberOfPlaysForPresseNovel()
        {
            return numberOfPlaysForPresseNovel;
        }

        public void SetNumberOfPlaysForBueroNovel(int numberOfPlaysForBueroNovel)
        {
            this.numberOfPlaysForBueroNovel = numberOfPlaysForBueroNovel;
        }

        public int GetNumberOfPlaysForBueroNovel()
        {
            return numberOfPlaysForBueroNovel;
        }

        public void SetNumberOfPlaysForGruendungszuschussNovel(int numberOfPlaysForGruendungszuschussNovel)
        {
            this.numberOfPlaysForGruendungszuschussNovel = numberOfPlaysForGruendungszuschussNovel;
        }

        public int GetNumberOfPlaysForGruenungszuschussNovel()
        {
            return numberOfPlaysForGruendungszuschussNovel;
        }

        public void SetNumberOfPlaysForHonorarNovel(int numberOfPlaysForHonorarNovel)
        {
            this.numberOfPlaysForHonorarNovel = numberOfPlaysForHonorarNovel;
        }

        public int GetNumberOfPlaysForHonorarNovel()
        {
            return numberOfPlaysForHonorarNovel;
        }

        public void SetNumberOfPlaysForLebenspartnerNovel(int numberOfPlaysForLebenspartnerNovel)
        {
            this.numberOfPlaysForLebenspartnerNovel = numberOfPlaysForLebenspartnerNovel;
        }

        public int GetNumberOfPlaysForLebenspartnerNovel()
        {
            return numberOfPlaysForLebenspartnerNovel;
        }

        public void SetNumberOfPlaysForIntroNovel(int numberOfPlaysForIntroNovel)
        {
            this.numberOfPlaysForIntroNovel = numberOfPlaysForIntroNovel;
        }

        public int GetNumberOfPlaysForIntroNovel()
        {
            return numberOfPlaysForIntroNovel;
        }

        public void ClearData()
        {
            numberOfPlaysForBankkreditNovel = 0;
            numberOfPlaysForBekannteTreffenNovel = 0;
            numberOfPlaysForBankkontoNovel = 0;
            numberOfPlaysForFoerderantragNovel = 0;
            numberOfPlaysForElternNovel = 0;
            numberOfPlaysForNotarinNovel = 0;
            numberOfPlaysForPresseNovel = 0;
            numberOfPlaysForBueroNovel = 0;
            numberOfPlaysForGruendungszuschussNovel = 0;
            numberOfPlaysForHonorarNovel = 0;
            numberOfPlaysForLebenspartnerNovel = 0;
            numberOfPlaysForIntroNovel = 0;
        }
    }
}