using Microsoft.AspNetCore.Identity;

public class UserModel : IdentityUser

{
    public long SysUserId { get; set; } // This can be the primary key.
    public long ClientId { get; set; }
    public string ClientType { get; set; }
    public string ClientName { get; set; }
    public string Groups { get; set; }

}
