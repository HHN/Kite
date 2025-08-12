using System;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    /// <summary>
    /// The PlayRecordManagerWrapper class provides functionality to manage the count of plays
    /// for different types of novels. It includes methods to get and set the number of plays
    /// for specific novel categories and to reset all the data.
    /// </summary>
    [Serializable]
    public class PlayRecordManagerWrapper
    {
        [SerializeField] private int numberOfPlaysForBankkreditNovel;
        [SerializeField] private int numberOfPlaysForInvestorNovel;
        [SerializeField] private int numberOfPlaysForElternNovel;
        [SerializeField] private int numberOfPlaysForNotarinNovel;
        [SerializeField] private int numberOfPlaysForPresseNovel;
        [SerializeField] private int numberOfPlaysForHonorarNovel;
        [SerializeField] private int numberOfPlaysForIntroNovel;

        /// <summary>
        /// Updates the number of plays for the Bankkredit novel to the specified value.
        /// </summary>
        /// <param name="numberOfPlaysForBankkreditNovel">
        /// The updated number of plays for the Bankkredit novel.
        /// </param>
        public void SetNumberOfPlaysForBankkreditNovel(int numberOfPlaysForBankkreditNovel)
        {
            this.numberOfPlaysForBankkreditNovel = numberOfPlaysForBankkreditNovel;
        }

        /// <summary>
        /// Retrieves the current number of plays for the Bankkredit novel.
        /// </summary>
        /// <returns>
        /// The total number of times the Bankkredit novel has been played.
        /// </returns>
        public int GetNumberOfPlaysForBankkreditNovel()
        {
            return numberOfPlaysForBankkreditNovel;
        }

        /// <summary>
        /// Updates the number of plays for the Investor novel to the specified value.
        /// </summary>
        /// <param name="numberOfPlaysForInvestorNovel">
        /// The updated number of plays for the Investor novel.
        /// </param>
        public void SetNumberOfPlaysForInvestorNovel(int numberOfPlaysForInvestorNovel)
        {
            this.numberOfPlaysForInvestorNovel = numberOfPlaysForInvestorNovel;
        }

        /// <summary>
        /// Retrieves the number of plays for the Investor novel.
        /// </summary>
        /// <returns>
        /// The number of plays for the Investor novel.
        /// </returns>
        public int GetNumberOfPlaysForInvestorNovel()
        {
            return numberOfPlaysForInvestorNovel;
        }

        /// <summary>
        /// Updates the number of plays for the Eltern novel to the specified value.
        /// </summary>
        /// <param name="numberOfPlaysForElternNovel">
        /// The updated number of plays for the Eltern novel.
        /// </param>
        public void SetNumberOfPlaysForElternNovel(int numberOfPlaysForElternNovel)
        {
            this.numberOfPlaysForElternNovel = numberOfPlaysForElternNovel;
        }

        /// <summary>
        /// Retrieves the current number of plays for the Eltern novel.
        /// </summary>
        /// <returns>
        /// The number of plays for the Eltern novel.
        /// </returns>
        public int GetNumberOfPlaysForElternNovel()
        {
            return numberOfPlaysForElternNovel;
        }

        /// <summary>
        /// Updates the number of plays for the Notarin novel to the specified value.
        /// </summary>
        /// <param name="numberOfPlaysForNotarinNovel">
        /// The updated number of plays for the Notarin novel.
        /// </param>
        public void SetNumberOfPlaysForNotarinNovel(int numberOfPlaysForNotarinNovel)
        {
            this.numberOfPlaysForNotarinNovel = numberOfPlaysForNotarinNovel;
        }

        /// <summary>
        /// Retrieves the current number of plays for the Notarin novel.
        /// </summary>
        /// <returns>
        /// The current count of plays for the Notarin novel.
        /// </returns>
        public int GetNumberOfPlaysForNotarinNovel()
        {
            return numberOfPlaysForNotarinNovel;
        }

        /// <summary>
        /// Updates the number of plays for the Presse novel to the specified value.
        /// </summary>
        /// <param name="numberOfPlaysForPresseNovel">
        /// The updated number of plays for the Presse novel.
        /// </param>
        public void SetNumberOfPlaysForPresseNovel(int numberOfPlaysForPresseNovel)
        {
            this.numberOfPlaysForPresseNovel = numberOfPlaysForPresseNovel;
        }

        /// <summary>
        /// Retrieves the number of plays for the Presse novel.
        /// </summary>
        /// <returns>
        /// The current number of plays for the Presse novel.
        /// </returns>
        public int GetNumberOfPlaysForPresseNovel()
        {
            return numberOfPlaysForPresseNovel;
        }

        /// <summary>
        /// Updates the number of plays for the Honorar novel to the specified value.
        /// </summary>
        /// <param name="numberOfPlaysForHonorarNovel">
        /// The updated number of plays for the Honorar novel.
        /// </param>
        public void SetNumberOfPlaysForHonorarNovel(int numberOfPlaysForHonorarNovel)
        {
            this.numberOfPlaysForHonorarNovel = numberOfPlaysForHonorarNovel;
        }

        /// <summary>
        /// Retrieves the number of plays recorded for the Honorar novel.
        /// </summary>
        /// <returns>
        /// The current count of plays for the Honorar novel.
        /// </returns>
        public int GetNumberOfPlaysForHonorarNovel()
        {
            return numberOfPlaysForHonorarNovel;
        }

        /// <summary>
        /// Updates the number of plays for the Intro novel to the specified value.
        /// </summary>
        /// <param name="numberOfPlaysForIntroNovel">
        /// The updated number of plays for the Intro novel.
        /// </param>
        public void SetNumberOfPlaysForIntroNovel(int numberOfPlaysForIntroNovel)
        {
            this.numberOfPlaysForIntroNovel = numberOfPlaysForIntroNovel;
        }

        /// <summary>
        /// Retrieves the number of plays recorded for the Intro novel.
        /// </summary>
        /// <returns>
        /// The current count of plays for the Intro novel.
        /// </returns>
        public int GetNumberOfPlaysForIntroNovel()
        {
            return numberOfPlaysForIntroNovel;
        }

        /// <summary>
        /// Resets the play counts for all novel categories to zero.
        /// </summary>
        public void ClearData()
        {
            numberOfPlaysForBankkreditNovel = 0;
            numberOfPlaysForInvestorNovel = 0;
            numberOfPlaysForElternNovel = 0;
            numberOfPlaysForNotarinNovel = 0;
            numberOfPlaysForPresseNovel = 0;
            numberOfPlaysForHonorarNovel = 0;
            numberOfPlaysForIntroNovel = 0;
        }
    }
}