using System.Reflection;
using System;
using System.Linq;

namespace ActiveDirectory
{
    public class AdUser 
    {
        string authorigbl { get; set; }
        long badpasswordtime { get; set; }
        string title { get; set; }
        string physicaldeliveryofficename { get; set; }
        long accountexpires { get; set; }
        string legacyexchangedn { get; set; }
        byte[] msexchmailboxguid { get; set; }
        string proxyaddresses { get; set; }
        long lastlogontimestamp { get; set; }
        string l { get; set; }
        DateTime dscorepropagationdata { get; set; }
        int samaccounttype { get; set; }
        int primarygroupid { get; set; }
        int useraccountcontrol { get; set; }
        string extensionattribute4 { get; set; }
        string msexcharchivename { get; set; }
        string extensionattribute3 { get; set; }
        string memberof { get; set; }
        string msexchpoliciesincluded { get; set; }
        long msexchrecipienttypedetails { get; set; }
        string c { get; set; }
        string cn { get; set; }
        string directreports { get; set; }
        long msexchversion { get; set; }
        string employeenumber { get; set; }
        int countrycode { get; set; }
        int codepage { get; set; }
        string description { get; set; }
        byte[] objectsid { get; set; }
        long pwdlastset { get; set; }
        int msexchrecipientdisplaytype { get; set; }
        int badpwdcount { get; set; }
        string homedrive { get; set; }
        string co { get; set; }
        DateTime whenchanged { get; set; }
        string msexchumdtmfmap { get; set; }
        string extensionattribute11 { get; set; }
        long usnchanged { get; set; }
        string manager { get; set; }
        string homedirectory { get; set; }
        string mail { get; set; }
        string msexchuserculture { get; set; }
        string accountnamehistory { get; set; }
        byte[] thumbnailphoto { get; set; }
        byte[] objectguid { get; set; }
        string givenname { get; set; }
        string adspath { get; set; }
        string distinguishedname { get; set; }
        string extensionattribute9 { get; set; }
        string displayname { get; set; }
        long lastlogoff { get; set; }
        string employeeid { get; set; }
        long lastlogon { get; set; }
        string postalcode { get; set; }
        string company { get; set; }
        string targetaddress { get; set; }
        string userparameters { get; set; }
        string st { get; set; }
        string ipphone { get; set; }
        string extensionattribute12 { get; set; }
        string initials { get; set; }
        string employeetype { get; set; }
        byte[] logonhours { get; set; }
        string otherpager { get; set; }
        string telephonenumber { get; set; }
        long msexchremoterecipienttype { get; set; }
        byte[] msexchsafesendershash { get; set; }
        string mailnickname { get; set; }
        string streetaddress { get; set; }
        string department { get; set; }
        byte[] sidhistory { get; set; }
        int logoncount { get; set; }
        string sn { get; set; }
        string objectcategory { get; set; }
        string userprincipalname { get; set; }
        string mobile { get; set; }
        DateTime whencreated { get; set; }
        string showinaddressbook { get; set; }
        byte[] msexcharchiveguid { get; set; }
        byte[] msexchblockedsendershash { get; set; }
        string samaccountname { get; set; }
        string name { get; set; }
        bool msexchhidefromaddresslists { get; set; }
        string managedobjects { get; set; }
        int msexchelcmailboxflags { get; set; }
        string objectclass { get; set; }
        long usncreated { get; set; }
        int instancetype { get; set; }

        public object GetPropertyValue(string propertyName)
        {
            return this.GetType().GetProperties()
                .Single(pi => pi.Name == propertyName)
                .GetValue(this, null);
        }

        // public void SetPropertyValue(string propertyName, object value)
        // {
        //     //var properties = this.GetType().GetProperties();
        //     //var properties = this.GetType().GetRuntimeProperties();
            
        //     this.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance).SetValue(this, value, null);
        // }
    }
    
    
    /* 
    public class AdUser
    {
        public string department { get; set; }
        public DateTime badpasswordtime { get; set; }
        public string msexchuserculture { get; set; }
        public string distinguishedname { get; set; }
        public string extensionattribute12 { get; set; }
        public string samaccounttype { get; set; }
        public string msexchelcmailboxflags { get; set; }
        public string cn { get; set; }
        public string msexchpoliciesincluded { get; set; }
        public string proxyaddresses { get; set; }
        public string msexcharchivename { get; set; }
        public string extensionattribute11 { get; set; }
        public string otherpager { get; set; }
        public string employeeid { get; set; }
        public string homedrive { get; set; }
        public string initials { get; set; }
        public string showinaddressbook { get; set; }
        public string objectsid { get; set; }
        public string msexchumdtmfmap { get; set; }
        public DateTime lastlogontimestamp { get; set; }
        public string adspath { get; set; }
        public string homedirectory { get; set; }
        public string dscorepropagationdata { get; set; }
        public string whenchanged { get; set; }
        public string l { get; set; }
        public string msexcharchiveguid { get; set; }
        public string lastlogoff { get; set; }
        public string userprincipalname { get; set; }
        public string company { get; set; }
        public string postalcode { get; set; }
        public DateTime lastlogon { get; set; }
        public string managedobjects { get; set; }
        public string sidhistory { get; set; }
        public DateTime pwdlastset { get; set; }
        public string countrycode { get; set; }
        public string logoncount { get; set; }
        public string employeenumber { get; set; }
        public string badpwdcount { get; set; }
        public string mobile { get; set; }
        public string memberof { get; set; }
        public string description { get; set; }
        public string displayname { get; set; }
        public string msexchsafesendershash { get; set; }
        public string msexchrecipienttypedetails { get; set; }
        public string extensionattribute4 { get; set; }
        public string c { get; set; }
        public string accountnamehistory { get; set; }
        public string usncreated { get; set; }
        public string msexchversion { get; set; }
        public string objectclass { get; set; }
        public string legacyexchangedn { get; set; }
        public string sn { get; set; }
        public string msexchhidefromaddresslists { get; set; }
        public string mail { get; set; }
        public string accountexpires { get; set; }
        public string usnchanged { get; set; }
        public string extensionattribute3 { get; set; }
        public string name { get; set; }
        public string employeetype { get; set; }
        public string samaccountname { get; set; }
        public string givenname { get; set; }
        public string targetaddress { get; set; }
        public string objectguid { get; set; }
        public string telephonenumber { get; set; }
        public string streetaddress { get; set; }
        public string codepage { get; set; }
        public string msexchblockedsendershash { get; set; }
        public string logonhours { get; set; }
        public string msexchremoterecipienttype { get; set; }
        public string userparameters { get; set; }
        public string thumbnailphoto { get; set; }
        public string useraccountcontrol { get; set; }
        public string st { get; set; }
        public string title { get; set; }
        public string physicaldeliveryofficename { get; set; }
        public string msexchmailboxguid { get; set; }
        public string co { get; set; }
        public string extensionattribute9 { get; set; }
        public string whencreated { get; set; }
        public string instancetype { get; set; }
        public string objectcategory { get; set; }
        public string authorigbl { get; set; }
        public string manager { get; set; }
        public string msexchrecipientdisplaytype { get; set; }
        public int directreports { get; set; }
        public string ipphone { get; set; }
        public string primarygroupid { get; set; }
        public string mailnickname { get; set; }

    }
    */
}