using System;

namespace ActiveDirectory
{
    public class ActiveDirectoryUser
    {
        public string AccountDescription { get; set; }
        public string DisplayName { get; set; }
        public string DistinguishedName { get; set; }
        public string EmailAddress { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public Guid? GuidId { get; set; }
        public bool IsAccountLockedOut { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? LastLogon { get; set; }
        public string LastName { get; set; }
        public string SAMAccountName { get; set; }
        public string Telephone { get; set; }

        //Additional properties
        public string Manager { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}~{10}~{11}~{12}~{13}~{14}", GuidId, DistinguishedName, SAMAccountName, DisplayName, EmployeeId, FirstName, LastName, EmailAddress, Telephone, AccountDescription, LastLogon, IsAccountLockedOut, IsEnabled, Manager, Title);
        }

        public string[] GetListOfValues()
        {
            return ToString().Split('~');
        }
    }
}