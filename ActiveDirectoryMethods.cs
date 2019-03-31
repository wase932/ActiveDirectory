using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Text;

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

        public string GetCreateTableDDL(string tableName, DataTable table)
        {
            var ddl = new StringBuilder();
            ddl.AppendLine($"create table [{tableName}] (");
            foreach (DataColumn col in table.Columns)
            {
                ddl.Append($"  [{col.ColumnName}] nvarchar(255), ");
            }
            ddl.Length = ddl.Length - ", ".Length;
            ddl.Append(")");

            return ddl.ToString();
        }

        public void LoadDatabase(string server, string database, string destinationTableName, DataTable table){
            
            // take note of SqlBulkCopyOptions.KeepIdentity , you may or may not want to use this for your situation.
            SqlConnection _connection = new SqlConnection(@"Data Source=" + server + ";Initial Catalog="+ database +";Integrated Security=SSPI;");  

                using (var bulkCopy = new SqlBulkCopy(_connection.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                    foreach (DataColumn col in table.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }

                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = destinationTableName;
                    bulkCopy.WriteToServer(table);
                }
            }

        public DataTable GetAllADUserValues(string domain){
            var allProperties = GetAllADUserProperties(domain);

            //Create Data Table
            var dataColumns = CreateColumns(allProperties);
            var table = CreateTable(dataColumns);

            using (var context = new PrincipalContext(ContextType.Domain, domain))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    var i = 0;
                    foreach (var result in searcher.FindAll())
                    {
                        // if(i == 150)
                        //     break;
                        i ++;
                        Console.WriteLine(i);
                        var userValues = new List<string>();
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        foreach (var p in allProperties)
                        {
                            string value = de.Properties[p].Value == null? string.Empty : de.Properties[p].Value.ToString(); 
                            if(value.Length > 255)
                                value = string.Empty;
                            // var value = de.Properties[p].Value?? string.Empty;
                            userValues.Add( value);
                            //Console.WriteLine("{0} : {1}", p,value);
                            //Console.WriteLine("{0} : {1}", p, de.Properties[p].Value);
                        }
                        LoadDataTable(table, userValues);
                    }
                }
            }
            return table;
        }

        public List<String> GetAllADUserProperties(string domain) //"dcs.azdcs.gov"
        {
                List<String> properties = new List<String>();
                IPAddress[] ips = Dns.GetHostAddresses(domain).Where(w => w.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();
                if (ips.Length > 0)
                {
                    DirectoryContext directoryContext = new DirectoryContext(DirectoryContextType.Forest);//DirectoryContextType.DirectoryServer, ips[0].ToString() + ":389", Username, Password);
                    ActiveDirectorySchema adschema = ActiveDirectorySchema.GetSchema(directoryContext);
                    ActiveDirectorySchemaClass adschemaclass = adschema.FindClass("User");

                    // Read the OptionalProperties & MandatoryProperties
                    ReadOnlyActiveDirectorySchemaPropertyCollection propcol = adschemaclass.GetAllProperties();

                    foreach (ActiveDirectorySchemaProperty schemaProperty in propcol)
                        properties.Add(schemaProperty.Name.ToLower());
                }

                return properties;
        }
    
    
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

        private void LoadDataTable(DataTable table, List<string> data)
        {
            var newRow = table.NewRow();
            for(int i =0; i < table.Columns.Count; i++){
                newRow[i] = data[i];
            }

            table.Rows.Add(newRow);

            // table.Rows.Add()
            // DataTable dt = new DataTable();

            // dt.Columns.Add("Name");
            // dt.Columns.Add("Price");
            // dt.Columns.Add("URL");

            // foreach (var item in list)
            // {
            //     var row = dt.NewRow();

            //     row["Name"] = item.Name;
            //     row["Price"] = Convert.ToString(item.Price);
            //     row["URL"] = item.URL;

            //     dt.Rows.Add(row);
            // }
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

        private List<DataColumn> CreateColumns(List<string> columnNames)
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
        public AdUser GetDirectoryEntry(string adUserName, string domainName) //"LDAP://dcs.azdcs.gov"
        {
            DirectoryEntry de = new DirectoryEntry(domainName);

            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + adUserName + "))";
            ds.SearchScope = SearchScope.Subtree;

            SearchResult rs = ds.FindOne();

            //lets try some reflection:
            var user = new AdUser();
            foreach (var p in rs.Properties)
            {
                dynamic item = p;
                string key = item.Key;
                var value = item.Value[0];
                
                var prop = user.GetType().GetRuntimeProperties().Single(i => i.Name == key);
                if(null != prop && prop.CanWrite)
                {
                    prop.SetValue(user, value, null);
                }
            }
            ds.Dispose();
            de.Dispose();
            return user;
        }

        public List<AdUser> GetAllActiveDirectoryAccounts(string domainName)//"LDAP://dcs.azdcs.gov"
        {
            DirectoryEntry de = new DirectoryEntry(domainName);

            DirectorySearcher ds = new DirectorySearcher(de);
            ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=*))";
            ds.SearchScope = SearchScope.Subtree;

            SearchResult rs = ds.FindOne();
            var result = new List<AdUser>();

            //lets try some reflection:
            foreach (var p in rs.Properties)
            {
                var user = new AdUser();
                dynamic item = p;
                string key = item.Key;
                var value = item.Value[0];
                
                var prop = user.GetType().GetRuntimeProperties().SingleOrDefault(i => i.Name == key);
                if(null != prop && prop.CanWrite)
                {
                    prop.SetValue(user, value, null);
                }

                result.Add(user);
            }
            ds.Dispose();
            de.Dispose();
            return result;
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