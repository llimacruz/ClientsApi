using ClientsApi.Models;

namespace ClientsApi.Business
{
    public static class ClientValidator
    {
        public static bool IsValid(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.Name))
                return false;

            if (!EmailValidator.IsValid(client.Email))
                return false;

            if (client.MaritalStatus != MaritalStatus.Married &&
                client.MaritalStatus != MaritalStatus.Single &&
                client.MaritalStatus != MaritalStatus.Divorced &&
                client.MaritalStatus != MaritalStatus.Widowed)
                return false;

            if (client.PhoneNumbersList.Count == 0)
                return false;

            if (string.IsNullOrWhiteSpace(client.Street))
                return false;

            if (string.IsNullOrWhiteSpace(client.City))
                return false;

            if (string.IsNullOrWhiteSpace(client.State))
                return false;

            if (string.IsNullOrWhiteSpace(client.Country))
                return false;

            if (string.IsNullOrWhiteSpace(client.Zip))
                return false;

            if (!CPFValidator.IsValid(client.CPF))
                return false;

            return true;
        }
    }
}