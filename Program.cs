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
                var user = new AdUser{
                            department =   rs.Properties["department"][0].ToString()
                            , badpasswordtime =  new DateTime(Int64.Parse(rs.Properties["badpasswordtime"][0].ToString()))
                            , msexchuserculture =   rs.Properties["msexchuserculture"][0].ToString()
                            , distinguishedname =   rs.Properties["distinguishedname"][0].ToString()
                            , extensionattribute12 =   rs.Properties["extensionattribute12"][0].ToString()
                            , samaccounttype =   rs.Properties["samaccounttype"][0].ToString()
                            , msexchelcmailboxflags =   rs.Properties["msexchelcmailboxflags"][0].ToString()
                            , cn =   rs.Properties["cn"][0].ToString()
                            , msexchpoliciesincluded =   rs.Properties["msexchpoliciesincluded"][0] .ToString()
                            , proxyaddresses =   rs.Properties["proxyaddresses"][0].ToString()
                            , msexcharchivename =   rs.Properties["msexcharchivename"][0].ToString()
                            , extensionattribute11 =   rs.Properties["extensionattribute11"][0].ToString()
                            , otherpager =   rs.Properties["otherpager"][0].ToString()
                            , employeeid =   rs.Properties["employeeid"][0].ToString()
                            , homedrive =   rs.Properties["homedrive"][0].ToString()
                            , initials =   rs.Properties["initials"][0].ToString()
                            , showinaddressbook =   rs.Properties["showinaddressbook"][0].ToString()
                            , objectsid =   rs.Properties["objectsid"][0].ToString()
                            , msexchumdtmfmap =   rs.Properties["msexchumdtmfmap"][0].ToString()
                            , lastlogontimestamp =  new DateTime(Int64.Parse(rs.Properties["lastlogontimestamp"][0].ToString()))
                            , adspath =   rs.Properties["adspath"][0].ToString()
                            , homedirectory =   rs.Properties["homedirectory"][0].ToString()
                            , dscorepropagationdata =   rs.Properties["dscorepropagationdata"][0].ToString()
                            , whenchanged =   rs.Properties["whenchanged"][0].ToString()
                            , l =   rs.Properties["l"][0].ToString()
                            , msexcharchiveguid =   rs.Properties["msexcharchiveguid"][0].ToString()
                            , lastlogoff =   rs.Properties["lastlogoff"][0].ToString()
                            , userprincipalname =   rs.Properties["userprincipalname"][0].ToString()
                            , company =   rs.Properties["company"][0].ToString()
                            , postalcode =   rs.Properties["postalcode"][0].ToString()
                            , lastlogon =   new DateTime(Int64.Parse(rs.Properties["lastlogon"][0].ToString()))
                            , managedobjects =   rs.Properties["managedobjects"][0].ToString()
                            , sidhistory =   rs.Properties["sidhistory"][0].ToString()
                            , pwdlastset =   new DateTime(Int64.Parse(rs.Properties["pwdlastset"][0].ToString()))
                            , countrycode =   rs.Properties["countrycode"][0].ToString()
                            , logoncount =   rs.Properties["logoncount"][0].ToString()
                            , employeenumber =   rs.Properties["employeenumber"][0].ToString()
                            , badpwdcount =   rs.Properties["badpwdcount"][0].ToString()
                            , mobile =   rs.Properties["mobile"][0].ToString()
                            , memberof =   rs.Properties["memberof"][0].ToString()
                            , description =   rs.Properties["description"][0].ToString()
                            , displayname =   rs.Properties["displayname"][0].ToString()
                            , msexchsafesendershash =   rs.Properties["msexchsafesendershash"][0].ToString()
                            , msexchrecipienttypedetails =   rs.Properties["msexchrecipienttypedetails"][0].ToString()
                            , extensionattribute4 =   rs.Properties["extensionattribute4"][0].ToString()
                            , c =   rs.Properties["c"][0].ToString()
                            , accountnamehistory =   rs.Properties["accountnamehistory"][0].ToString()
                            , usncreated =   rs.Properties["usncreated"][0].ToString()
                            , msexchversion =   rs.Properties["msexchversion"][0].ToString()
                            , objectclass =   rs.Properties["objectclass"][0].ToString()
                            , legacyexchangedn =   rs.Properties["legacyexchangedn"][0].ToString()
                            , sn =   rs.Properties["sn"][0].ToString()
                            , msexchhidefromaddresslists =   rs.Properties["msexchhidefromaddresslists"][0].ToString()
                            , mail =   rs.Properties["mail"][0].ToString()
                            , accountexpires =   rs.Properties["accountexpires"][0].ToString()
                            , usnchanged =   rs.Properties["usnchanged"][0].ToString()
                            , extensionattribute3 =   rs.Properties["extensionattribute3"][0].ToString()
                            , name =   rs.Properties["name"][0].ToString()
                            , employeetype =   rs.Properties["employeetype"][0].ToString()
                            , samaccountname =   rs.Properties["samaccountname"][0].ToString()
                            , givenname =   rs.Properties["givenname"][0].ToString()
                            , targetaddress =   rs.Properties["targetaddress"][0].ToString()
                            , objectguid =   rs.Properties["objectguid"][0].ToString()
                            , telephonenumber =   rs.Properties["telephonenumber"][0].ToString()
                            , streetaddress =   rs.Properties["streetaddress"][0].ToString()
                            , codepage =   rs.Properties["codepage"][0].ToString()
                            , msexchblockedsendershash =   rs.Properties["msexchblockedsendershash"][0].ToString()
                            , logonhours =   rs.Properties["logonhours"][0].ToString()
                            , msexchremoterecipienttype =   rs.Properties["msexchremoterecipienttype"][0].ToString()
                            , userparameters =   rs.Properties["userparameters"][0].ToString()
                            , thumbnailphoto =   rs.Properties["thumbnailphoto"][0].ToString()
                            , useraccountcontrol =   rs.Properties["useraccountcontrol"][0].ToString()
                            , st =   rs.Properties["st"][0].ToString()
                            , title =   rs.Properties["title"][0].ToString()
                            , physicaldeliveryofficename =   rs.Properties["physicaldeliveryofficename"][0].ToString()
                            , msexchmailboxguid =   rs.Properties["msexchmailboxguid"][0].ToString()
                            , co =   rs.Properties["co"][0].ToString()
                            , extensionattribute9 =   rs.Properties["extensionattribute9"][0].ToString()
                            , whencreated =   rs.Properties["whencreated"][0].ToString()
                            , instancetype =   rs.Properties["instancetype"][0].ToString()
                            , objectcategory =   rs.Properties["objectcategory"][0].ToString()
                            , authorigbl =   rs.Properties["authorigbl"][0].ToString()
                            , manager =   rs.Properties["manager"][0].ToString()
                            , msexchrecipientdisplaytype =   rs.Properties["msexchrecipientdisplaytype"][0].ToString()
                            , directreports =   rs.Properties["directreports"].Count //counting this for now...
                            , ipphone =   rs.Properties["ipphone"][0].ToString()
                            , primarygroupid =   rs.Properties["primarygroupid"][0].ToString()
                            , mailnickname =   rs.Properties["mailnickname"][0].ToString()
                            };
            }
        }

        
    }
}
