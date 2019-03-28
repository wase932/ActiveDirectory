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
            //GetActiveDirectoryAccount("dcs.azdcs.gov");
            
            GetDirectoryEntry("D046113", "LDAP://dcs.azdcs.gov");
            
            //Console.WriteLine("Hello World!");
            
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

    public static void GetDirectoryEntry(string adUserName, string domainName)
    {
        DirectoryEntry de = new DirectoryEntry(domainName);

        DirectorySearcher ds = new DirectorySearcher(de);
        ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + adUserName + "))";
        ds.SearchScope = SearchScope.Subtree;

        SearchResult rs = ds.FindOne();
        
        //get all properties:
        for (int i = 0; i < rs.Properties.Count; i++)
        {
            var   department =   rs.Properties["department"][0];
            var   badpasswordtime =   rs.Properties["badpasswordtime"][0];
            var   msexchuserculture =   rs.Properties["msexchuserculture"][0];
            var   distinguishedname =   rs.Properties["distinguishedname"][0];
            var   extensionattribute12 =   rs.Properties["extensionattribute12"][0];
            var   samaccounttype =   rs.Properties["samaccounttype"][0];
            var   msexchelcmailboxflags =   rs.Properties["msexchelcmailboxflags"][0];
            var   cn =   rs.Properties["cn"][0];
            var   msexchpoliciesincluded =   rs.Properties["msexchpoliciesincluded"][0]; 
            var   proxyaddresses =   rs.Properties["proxyaddresses"][0];
            var   msexcharchivename =   rs.Properties["msexcharchivename"][0];
            var   extensionattribute11 =   rs.Properties["extensionattribute11"][0];
            var   otherpager =   rs.Properties["otherpager"][0];
            var   employeeid =   rs.Properties["employeeid"][0];
            var   homedrive =   rs.Properties["homedrive"][0];
            var   initials =   rs.Properties["initials"][0];
            var   showinaddressbook =   rs.Properties["showinaddressbook"][0];
            var   objectsid =   rs.Properties["objectsid"][0];
            var   msexchumdtmfmap =   rs.Properties["msexchumdtmfmap"][0];
            var   lastlogontimestamp =   rs.Properties["lastlogontimestamp"][0];
            var   adspath =   rs.Properties["adspath"][0];
            var   homedirectory =   rs.Properties["homedirectory"][0];
            var   dscorepropagationdata =   rs.Properties["dscorepropagationdata"][0];
            var   whenchanged =   rs.Properties["whenchanged"][0];
            var   l =   rs.Properties["l"][0];
            var   msexcharchiveguid =   rs.Properties["msexcharchiveguid"][0];
            var   lastlogoff =   rs.Properties["lastlogoff"][0];
            var   userprincipalname =   rs.Properties["userprincipalname"][0];
            var   company =   rs.Properties["company"][0];
            var   postalcode =   rs.Properties["postalcode"][0];
            var   lastlogon =   rs.Properties["lastlogon"][0];
            var   managedobjects =   rs.Properties["managedobjects"][0];
            var   sidhistory =   rs.Properties["sidhistory"][0];
            var   pwdlastset =   rs.Properties["pwdlastset"][0]; //timestamp
            var   countrycode =   rs.Properties["countrycode"][0];
            var   logoncount =   rs.Properties["logoncount"][0];
            var   employeenumber =   rs.Properties["employeenumber"][0];
            var   badpwdcount =   rs.Properties["badpwdcount"][0];
            var   mobile =   rs.Properties["mobile"][0];
            var   memberof =   rs.Properties["memberof"][0];
            var   description =   rs.Properties["description"][0];
            var   displayname =   rs.Properties["displayname"][0];
            var   msexchsafesendershash =   rs.Properties["msexchsafesendershash"][0];
            var   msexchrecipienttypedetails =   rs.Properties["msexchrecipienttypedetails"][0];
            var   extensionattribute4 =   rs.Properties["extensionattribute4"][0];
            var   c =   rs.Properties["c"][0];
            var   accountnamehistory =   rs.Properties["accountnamehistory"][0];
            var   usncreated =   rs.Properties["usncreated"][0];
            var   msexchversion =   rs.Properties["msexchversion"][0];
            var   objectclass =   rs.Properties["objectclass"][0];
            var   legacyexchangedn =   rs.Properties["legacyexchangedn"][0];
            var   sn =   rs.Properties["sn"][0];
            var   msexchhidefromaddresslists =   rs.Properties["msexchhidefromaddresslists"][0];
            var   mail =   rs.Properties["mail"][0];
            var   accountexpires =   rs.Properties["accountexpires"][0];
            var   usnchanged =   rs.Properties["usnchanged"][0];
            var   extensionattribute3 =   rs.Properties["extensionattribute3"][0];
            var   name =   rs.Properties["name"][0];
            var   employeetype =   rs.Properties["employeetype"][0];
            var   samaccountname =   rs.Properties["samaccountname"][0];
            var   givenname =   rs.Properties["givenname"][0];
            var   targetaddress =   rs.Properties["targetaddress"][0];
            var   objectguid =   rs.Properties["objectguid"][0];
            var   telephonenumber =   rs.Properties["telephonenumber"][0];
            var   streetaddress =   rs.Properties["streetaddress"][0];
            var   codepage =   rs.Properties["codepage"][0];
            var   msexchblockedsendershash =   rs.Properties["msexchblockedsendershash"][0];
            var   logonhours =   rs.Properties["logonhours"][0];
            var   msexchremoterecipienttype =   rs.Properties["msexchremoterecipienttype"][0];
            var   userparameters =   rs.Properties["userparameters"][0];
            var   thumbnailphoto =   rs.Properties["thumbnailphoto"][0];
            var   useraccountcontrol =   rs.Properties["useraccountcontrol"][0];
            var   st =   rs.Properties["st"][0];
            var   title =   rs.Properties["title"][0];
            var   physicaldeliveryofficename =   rs.Properties["physicaldeliveryofficename"][0];
            var   msexchmailboxguid =   rs.Properties["msexchmailboxguid"][0];
            var   co =   rs.Properties["co"][0];
            var   extensionattribute9 =   rs.Properties["extensionattribute9"][0];
            var   whencreated =   rs.Properties["whencreated"][0];
            var   instancetype =   rs.Properties["instancetype"][0];
            var   objectcategory =   rs.Properties["objectcategory"][0];
            var   authorigbl =   rs.Properties["authorigbl"][0];
            var   manager =   rs.Properties["manager"][0];
            var   msexchrecipientdisplaytype =   rs.Properties["msexchrecipientdisplaytype"][0];
            var   directreports =   rs.Properties["directreports"][0];
            var   ipphone =   rs.Properties["ipphone"][0];
            var   primarygroupid =   rs.Properties["primarygroupid"][0];
            var   mailnickname =   rs.Properties["mailnickname"][0];

        }

        foreach (string objProperty in rs.Properties["DirectReports"])
        {
            //isManager = true;
            string emp = objProperty.ToString();
            string[] setp = new string[1];
            setp[0] = "DC"; //If your users are in a OU use OU 

            emp = emp.Split(setp, StringSplitOptions.None)[0];
            emp = emp.Replace("CN=", "");
            emp = emp.TrimEnd(',');
            emp = emp.Replace("\\, ", ", ");
            emp = emp.Split(',')[0];
            //emps.Add(emp);
        }

    }
        
    }
}
