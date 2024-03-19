using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Shared.Domain;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Client.NU.Tests.Users.Domain.Aggregate
{
    [TestFixture]
    public class UserNUTests
    {

        private User existingUser;
        private Email email;
        [SetUp]
        public void Setup()
        {
            existingUser = User.Create(Guid.NewGuid(), "1725515348", "Paul Plaza", "", "", Guid.NewGuid(), email).Value;
        }

        [TestCase("485dae86-7ce6-41dc-9e34-4f092588027d", "Paul Plaza")]
        public void CreateUser_Input_GUID_DisplayName_From_ActiveDirectory_ReturnOk(string id, string? displayName)
        {
            Guid userInSession = Guid.NewGuid();
            var ensureUserCreated = User.Create(Guid.Parse(id), "1725515348", displayName, "", "", userInSession, email);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(ensureUserCreated.IsSuccess);
                Assert.AreEqual(displayName, ensureUserCreated.Value.DisplayName);
                Assert.AreEqual(string.Empty, ensureUserCreated.Value.Identification);
                Assert.AreEqual(UserType.Invited, ensureUserCreated.Value.UserType);
                Assert.AreEqual(Status.Active, ensureUserCreated.Value.Status);
                Assert.AreEqual(userInSession, ensureUserCreated.Value.CreatedBy);
                Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(ensureUserCreated.Value.CreatedOn));
            });
        }


        [TestCase("Dennys Paul Plaza ")]
        [TestCase("ppla")]
        public void Validation_Create_User_Input_Invalid(string displayName)
        {
            Guid userInSession = Guid.NewGuid();
            var ensureUserCreated = User.Create(Guid.NewGuid(), "1725515348", displayName, "", "", userInSession, email);
            string error = ValidationConstants.ValidateMaxAndMinLength("Nombre a mostrar", User.DisplayMinLength, User.DisplayMaxLength);
            Console.WriteLine(error);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(ensureUserCreated.IsFailure);
                Assert.That(displayName.Length < User.DisplayMinLength || displayName.Length > User.DisplayMaxLength,
                    error);
            });
        }


        [TestCase("485dae86-7ce6-41dc-9e34-4f092588027d", "")]
        [TestCase("", "Paul Plaza")]
        [TestCase("", "")]
        public void Throw_Create_User_Input_Invalid(string id, string displayName)
        {
            Guid idGuid = string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
            var exception = Assert.Throws<ArgumentException>(() => User.Create(idGuid, "1725515348", displayName, "", "", Guid.NewGuid(), email));

            if (string.IsNullOrWhiteSpace(id))
            {
                Assert.AreEqual("Required input id was empty. (Parameter 'id')", exception.Message);
                return;
            }

            if (string.IsNullOrWhiteSpace(displayName))
            {
                Assert.AreEqual("Required input displayName was empty. (Parameter 'displayName')", exception.Message);
            }
        }

        [TestCase("Dennys Paul", "1725515348", true)]
        [TestCase("Dennys Plaza", "1725515349", false)]
        public void Update_User_Input_DisplayName_Identification_ConvertToInvestorOrStarted(string displayName, string identification, bool isInvestor)
        {
            //TODO: construir mock para obtener cedula de inversor
            Guid userInSession = Guid.NewGuid();
            var ensuredUserUpdated = existingUser.UpdateInformation(displayName, identification, "", "", isInvestor, userInSession);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(ensuredUserUpdated.IsSuccess);
                Assert.AreEqual(displayName, existingUser.DisplayName);
                Assert.AreEqual(identification, existingUser.Identification);
                Assert.AreEqual(Status.Active, existingUser.Status);
                Assert.AreEqual(userInSession, existingUser.UpdatedBy);
                Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(existingUser.UpdatedOn.Value));

                //verifico si es inversionista o no
                Assert.AreEqual(isInvestor ? UserType.Investor : UserType.Invited, UserType.From(existingUser.UserType.Value));
            });

        }

        [TestCase("Dennys Paul Plaza ", "1725354768")]
        [TestCase("ppl", "1725354768")]
        [TestCase("Paul Plaza", "172")]
        [TestCase("ppl", "17253547681725354768")]
        public void Validation_Update_User_Input_Invalid(string displayName, string identification)
        {

            Guid userInSession = Guid.NewGuid();
            var ensuredUserUpdated = existingUser.UpdateInformation(displayName, identification, "", "", true, userInSession);


            string errorDisplayName = ValidationConstants.ValidateMaxAndMinLength("Nombre a mostrar", User.DisplayMinLength, User.DisplayMaxLength);
            Console.WriteLine(errorDisplayName);

            string errorIdentification = ValidationConstants.ValidateMaxAndMinLength("Identificación", User.IdentificationMinLength, User.IdentificationMaxLength);
            Console.WriteLine(errorIdentification);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(ensuredUserUpdated.IsFailure);

                Console.WriteLine(ensuredUserUpdated.Error);

                // Verifica que la longitud de displayName sea válida
                bool showValidationDisplayName = displayName.Length < User.DisplayMinLength || displayName.Length > User.DisplayMaxLength;
                bool showValidationIdentification = identification.Length <= User.IdentificationMinLength || identification.Length >= User.IdentificationMaxLength;
                if (showValidationIdentification && showValidationDisplayName)
                {
                    var errors = new string[]
                    {
                        errorDisplayName, errorIdentification
                    };
                    string message = string.Join(Environment.NewLine, errors);
                    Assert.That(ensuredUserUpdated.Error, Is.EqualTo(message));
                    return;
                }

                //valido primero si son todos por que es un listado de mensajes
                if (showValidationDisplayName)
                {
                    Assert.AreEqual(errorDisplayName, ensuredUserUpdated.Error);
                }

                // Verifica que la longitud de identification sea válida
                if (showValidationIdentification)
                {
                    Assert.AreEqual(errorIdentification, ensuredUserUpdated.Error);
                }

            });
        }

        [TestCase("Dennys Plaza", "")]
        [TestCase("", "Paul Plaza")]
        [TestCase("", "")]
        public void Throw_Update_User_Input_DisplayName_Identification_ConvertToUserStartedOrInvestor(string displayName, string identification)
        {
            var exception = Assert.Throws<ArgumentException>(() => existingUser.UpdateInformation(displayName, identification, "", "", true, Guid.NewGuid()));

            if (string.IsNullOrWhiteSpace(displayName))
            {
                Assert.AreEqual("Required input displayName was empty. (Parameter 'displayName')", exception.Message);
                return;
            }

            if (string.IsNullOrWhiteSpace(identification))
            {
                Assert.AreEqual("Required input identification was empty. (Parameter 'identification')", exception.Message);
            }
        }
    }
}