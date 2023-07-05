using Microsoft.AspNetCore.Identity;

namespace StudentProject.Models;

public class UserRole : IdentityRole<int>
{
}

public class UserEntity : IdentityUser<int>
{
}