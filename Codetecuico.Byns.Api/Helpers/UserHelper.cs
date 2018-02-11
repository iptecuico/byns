using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Domain;

namespace Codetecuico.Byns.Api.Helpers
{
    public static class UserHelper
    {
        public static bool IsUserInvalid(User user)
        {
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public static bool IsUserInvalid(UserModel user)
        {
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public static bool IsUserInvalid(int id)
        {
            if (id <= 0)
            {
                return true;
            }
            return false;
        }
    }
}