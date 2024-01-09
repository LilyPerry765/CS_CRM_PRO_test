namespace CaseManagement
{
    using Administration;
    using Serenity;
    using Serenity.Abstractions;
    using Serenity.Data;
    using System;
    using System.Configuration;

    public static partial class SiteInitialization
    {
        public static void ApplicationStart()
        {
            try
            {
                SqlSettings.AutoQuotedIdentifiers = true;
                Serenity.Web.CommonInitialization.Run();

                var registrar = Dependency.Resolve<IDependencyRegistrar>();
                registrar.RegisterInstance<IAuthorizationService>(new Administration.AuthorizationService());
                registrar.RegisterInstance<IAuthenticationService>(new Administration.AuthenticationService());
                registrar.RegisterInstance<IPermissionService>(new Administration.PermissionService());
                registrar.RegisterInstance<IUserRetrieveService>(new Administration.UserRetrieveService());

                if (!ConfigurationManager.AppSettings["LDAP"].IsTrimmedEmpty())
                    registrar.RegisterInstance<IDirectoryService>(new LdapDirectoryService());

                if (!ConfigurationManager.AppSettings["ActiveDirectory"].IsTrimmedEmpty())
                    registrar.RegisterInstance<IDirectoryService>(new ActiveDirectoryService());

                Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHmISksHQycUL29VsCU/X49Uxe3AUt/8k5pyAAeZU2yTNbag5e" +
"cOvE7R3OvI4AvbllUVVz6++gj5aGiNXeBRyoWCJxs8pO8mrMRLgtOg8MZKpGtIVh29g3SPFTvjv/zO5dhgqIhRq2GO" +
"zA8EnDkA420+Sud8kKorptNGlbanv8AF+eNpcdIbEI6y9sUru2wxaMhMh6zy3pvdKdwfShhmkvgLlTYJOevkkHAAIw" +
"+V2BWydeGg30Vf6R/SVuAu4OjG0hq7Xa6VX0PXQKHWDukTgiq3hXIpJsHJsXy/KA6rvTEnzrxIKhTWIzM0YNHj+ID2" +
"BYB+uN+ZdUk9mxCJLwdwM6ouW4AGP55j0YEArIn93g8MpkSCz0vGRkqaeVd8fEEmi89NyghvRSCcdfOfFCbtLGFXBz" +
"IIi/WBfXqy49VeUFkZ6hWL2LNVvJTygFOMgdEsWLT+MtfsShPoSdbSt4ogtPJNqvsogZebG4OJSIK50ieBpKlgmVN7" +
"vo/gHZrwd2c/xphUOi2vcstc4T7bXg1cr0gW";
            }
            catch (Exception ex)
            {
                ex.Log();
                throw;
            }

            foreach (var databaseKey in databaseKeys)
            {
                EnsureDatabase(databaseKey);
                RunMigrations(databaseKey);
            }
        }

        public static void ApplicationEnd()
        {
        }
    }
}