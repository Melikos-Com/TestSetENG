using Ptc.AspNet.Identity.database;
using System.Linq;
namespace Ptc.AspNet.Identity
{
    /// <summary>
    /// Class that represents the Role table in the MySQL Database
    /// </summary>
    public class RoleTable 
    {
        private SetengUser _database;

        /// <summary>
        /// Constructor that takes a MySQLDatabase instance 
        /// </summary>
        /// <param name="database"></param>
        public RoleTable(SetengUser database)
        {
            _database = database;
        }

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public int Delete(string Role_Id, string Comp_Cd)
        {


            var Role = _database.TSYSROL.AsQueryable().FirstOrDefault(x => x.Comp_Cd == Comp_Cd && x.Role_Id == Role_Id);

            _database.TSYSROL.Remove(Role);

            return _database.SaveChanges();
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="roleName">The role's name</param>
        /// <returns></returns>
        public int Insert(IdentityRole role)
        {


            TSYSROL Role = new TSYSROL()
            {
                Role_Id = role.Id,
                Comp_Cd = role.Comp_Cd,               
            };

            _database.TSYSROL.Add(Role);

            return _database.SaveChanges();
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(string roleId , string compcd)
        {
  

            return _database.TSYSROL.AsQueryable().Where(x => x.Comp_Cd == compcd && x.Role_Id == roleId).Select(g=>g.Role_Name).FirstOrDefault();

        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="roleName">Role's name</param>
        /// <returns>Role's Id</returns>
        public string GetRoleId(string roleName)
        {

            return "";
        }

        /// <summary>
        /// Gets the IdentityRole given the role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IdentityRole GetRoleById(string roleId, string compcd)
        {
            var roleName = GetRoleName(roleId, compcd);
            IdentityRole role = null;

            if(roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;

        }

        /// <summary>
        /// Gets the IdentityRole given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        public int Update(IdentityRole role)
        {


            var Role = _database.TSYSROL.AsQueryable().Where(x => x.Comp_Cd == role.Comp_Cd && x.Role_Id == role.Role_Id).FirstOrDefault();
            if (Role != null)
            {
                Role.Role_Name = role.Name;
            }

            return _database.SaveChanges();
        }
    }
}
