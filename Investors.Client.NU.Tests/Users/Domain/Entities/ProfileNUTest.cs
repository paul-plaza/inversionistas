using CSharpFunctionalExtensions;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.Entities.Profiles;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Domain.ValueObjects;
using System;

namespace Investors.Client.NU.Tests.Users.Domain.Entities
{
    public class ProfileNUTest
    {
        private Profile existingProfile;
        [SetUp]
        public void Setup()
        {
            existingProfile = Profile.Create(Guid.NewGuid()).Value;
        }

        [TestCase]
        public void Create_Initial_Profile_Input_User_OK()
        {

            Guid userInSession = Guid.NewGuid();
            int initialValueForAllProfile = 0;
            Result<Profile> ensureProfileCreated = Profile.Create(userInSession);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(ensureProfileCreated.IsSuccess);
                Assert.AreEqual(ensureProfileCreated.Value.CashBackToRedeem, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.AccumulativeNights, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.NightsToRedeem, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.HistoryCashBack, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.HistoryNights, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.TotalAccumulatedInvoice, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.TotalMonthlyCashBackToRedeem, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.TotalMonthlyCashBackClaimed, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.TotalCashBackClaimed, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.TotalMonthlyNightsClaimed, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.TotalMonthlyNightsToRedeem, initialValueForAllProfile);
                Assert.AreEqual(ensureProfileCreated.Value.TotalNightsClaimed, initialValueForAllProfile);

                Assert.AreEqual(ensureProfileCreated.Value.Category, UserCategory.Started);
                Assert.AreEqual(Status.From(ensureProfileCreated.Value.Status.Value), Status.Active);
                Assert.AreEqual(ensureProfileCreated.Value.CreatedBy, userInSession);
                Assert.AreEqual(DateOnly.FromDateTime(ensureProfileCreated.Value.CreatedOn), DateOnly.FromDateTime(DateTime.Now));
            });
        }

        [TestCase]
        public void IncreasePointInCashback_Input_Price()
        {

        }

        [TestCase(2)]
        [TestCase(12)]
        [TestCase(33)]
        public void AccumulateNights_Input_NumberNights_Ok(int numberNights)
        {
            Guid userInSession = Guid.NewGuid();
            var resultAccumulateNights = existingProfile.AccumulativeNights + numberNights;
            var resultNightsToRedeem = resultAccumulateNights / Profile.NightsFree;
            if (resultAccumulateNights > 10)
            {
                resultAccumulateNights = resultAccumulateNights % (resultNightsToRedeem * Profile.NightsFree); // reiniciar el contador de noches
            }
            var resultHistoryNights = existingProfile.HistoryNights + numberNights;
            var ensureAccumulateNights = existingProfile.AccumulateNights(numberNights, "", 0, userInSession);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(ensureAccumulateNights.IsSuccess);
                Assert.AreEqual(existingProfile.AccumulativeNights, resultAccumulateNights);
                Assert.AreEqual(existingProfile.NightsToRedeem, resultNightsToRedeem);
                Assert.AreEqual(existingProfile.HistoryNights, resultHistoryNights);
            });
        }

        [TestCase(-2)]
        [TestCase(120)]
        [TestCase(0)]
        public void Throw_AcumulateNights_Input_NumberNights(int numberNights)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => existingProfile.AccumulateNights(numberNights, "", 0, Guid.NewGuid()));

            Assert.AreEqual("Input numberNights was out of range (Parameter 'numberNights')", exception.Message);
        }
    }
}