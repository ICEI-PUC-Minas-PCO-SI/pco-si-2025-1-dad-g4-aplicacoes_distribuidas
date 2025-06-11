using API.Data;

namespace API.Service
{
    public static class OrderService
    {
        public async static Task<string> GetUserNameById(int id, ApplicationDbContext context)
        {
            var user = await context.Order.FindAsync(id);

            if (user == null) 
                throw new Exception("User not found");

            return user.Customer;
        }
    }
}
