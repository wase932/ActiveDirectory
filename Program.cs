using System.Linq;
using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Collections.Generic;

namespace ActiveDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            var domainName = "dcs.azdcs.gov";
            var ldapDomainName = "LDAP://dcs.azdcs.gov";
            //GetActiveDirectoryAccount("dcs.azdcs.gov");
            var activeDirectoryMethods = new ActiveDirectoryMethods();
            // var aduser = activeDirectoryMethods.GetDirectoryEntry("D046113", ldapDomainName);
            // var allUsers = activeDirectoryMethods.GetAllActiveDirectoryAccounts(ldapDomainName);
            //var allADUsers  = activeDirectoryMethods.GetAccounts(domainName, ldapDomainName);
            //activeDirectoryMethods.GetAllADUserProperties("dcs.azdcs.gov");
            var dataTable = activeDirectoryMethods.GetAllADUserValues(domainName);
            //var createTableDDL = activeDirectoryMethods.GetCreateTableDDL("bulkActiveDirectoryAccounts", dataTable);
            activeDirectoryMethods.LoadDatabase(@"GuardianMig01P\DeIdentified", "Staging_Exchanges", "bulkActiveDirectoryAccounts", dataTable);

            //For testing...
            //var titleFormat = string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}~{14}", "GuidId", "DistinguishedName", "SAMAccountName", "DisplayName", "EmployeeId", "FirstName", "LastName", "EmailAddress", "Telephone", "AccountDescription", "LastLogon", "IsAccountLockedOut", "IsEnabled", "Manager", "Title");
            //Console.WriteLine(titleFormat);

            // foreach (var i in allADUsers)
            // {
            //     Console.WriteLine( i.ToString());
            // }
            //adM.GetDirectoryEntry("D046113", "LDAP://dcs.azdcs.gov");
            
        }

        private static void GetActiveDirectoryAccount(string domainName){
            var context = new PrincipalContext(ContextType.Domain, domainName);
            var searcher = new PrincipalSearcher(new UserPrincipal(context));
            var userPrincipals = searcher.FindAll();
            
            foreach (var u in userPrincipals)
            {
                var groups = u.GetGroups();
                var underlyingObject = u.GetUnderlyingObject();
                var underlyingObjectType = u.GetUnderlyingObjectType();
                var structuralClass = u.StructuralObjectClass;
            }
        }
    }
}
