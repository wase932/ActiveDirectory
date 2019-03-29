using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ActiveDirectory
{
    public class ActiveDirectoryMethods
    {
        // private void LoadDTSVariable()
        // {
        //     var tableColumns = CreateColumns(new string[] { "GuidId", "DistinguishedName", "SAMAccountName", "DisplayName", "EmployeeId", "FirstName", "LastName", "EmailAddress", "Telephone", "AccountDescription", "LastLogon", "IsAccountLockedOut", "IsEnabled" });
        //     var dataTable = CreateTable(tableColumns);
        //     GetAccounts().ForEach(x =>
        //     {
        //         LoadDataTable(dataTable, x.GetListOfValues());
        //     });

        //     Dts.Variables["User::ADDataTable"].Value = dataTable;
        // }
    
    
        public List<ActiveDirectoryUser> GetAccounts(string domainName, string LDAPDomainName)
        {
            var result = new List<ActiveDirectoryUser>();
            using (var context = new PrincipalContext(ContextType.Domain, domainName)) //"dcs.azdcs.gov"
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (UserPrincipal u in searcher.FindAll())
                    {
                        
                        result.Add(new ActiveDirectoryUser
                        {
                            GuidId = u.Guid,
                            DistinguishedName = u.DistinguishedName,
                            SAMAccountName = u.SamAccountName,
                            DisplayName = u.DisplayName,
                            EmployeeId = u.EmployeeId,
                            FirstName = u.GivenName,
                            LastName = u.Surname,
                            EmailAddress = u.EmailAddress,
                            Telephone = u.VoiceTelephoneNumber,
                            AccountDescription = u.Description,
                            LastLogon = u.LastLogon,
                            IsAccountLockedOut = u.IsAccountLockedOut(),
                            IsEnabled = u.Enabled, 
                            Manager = GetActiveDirectoryProperty(u.SamAccountName, LDAPDomainName, "manager"),
                            Title = GetActiveDirectoryProperty(u.SamAccountName, LDAPDomainName, "title")
                        });
                    }
                }
            }
            return result;
        }

        private void LoadDataTable(DataTable table, string[] data)
        {
            table.Rows.Add(data);
        }

        private DataTable CreateTable(List<DataColumn> dataColumns)
        {
            var dataTable = new DataTable();
            dataTable.Columns.AddRange(dataColumns.ToArray());
            return dataTable;
        }

        private List<DataColumn> CreateColumns(string[] columnNames)
        {
            return columnNames.Select(x => new DataColumn
            {
                ColumnName = x,
                AllowDBNull = true
            }).ToList();
        }

        #region AllProperties
        //This method is an over kill at the moment - but could be very useful in the future.
        //Think about using reflection to resolve the property names so code does not break if AD Administrator changes structure (invalid since code will still break when trying to load up into database...)
        public void GetDirectoryEntry(string adUserName, string domainName) //"LDAP://dcs.azdcs.gov"
        {
            DirectoryEntry de = new DirectoryEntry(domainName);

            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + adUserName + "))";
            ds.SearchScope = SearchScope.Subtree;

            SearchResult rs = ds.FindOne();
            
            //get all properties:
            //TODO: try cast before retrieving value.
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

        #endregion AllProperties

        //TODO: Clean up these helper methods pre deployment

        #region TempHelper Methods

        public string GetActiveDirectoryProperty(string samAccountName, string domainName, string propertyName){
            DirectoryEntry de = new DirectoryEntry(domainName);

                DirectorySearcher ds = new DirectorySearcher(de);
                ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + samAccountName + "))";
                ds.SearchScope = SearchScope.Subtree;
            try{
                SearchResult rs = ds.FindOne();

                var result = rs.Properties[propertyName][0].ToString(); //see AdUser object for available properties (this is tacky, but I don't want to serialize the entire object)
                
                return result; //any consumer of this method will have to know that this is a temp fix, and you must cast/serialize complex objects where necessary
                
                //clean up when done. 
                //TODO: (change to using block before pushing to PROD!)
            }
            catch(Exception ex){
                //TODO log exception
                return null;
            }
            finally{
                ds.Dispose();
                de.Dispose();
            }
        }

        #endregion TempHelper Methods


    }
}