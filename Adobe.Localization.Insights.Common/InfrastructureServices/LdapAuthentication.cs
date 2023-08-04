using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;

namespace Adobe.Localization.Insights.Common
{
    public class LdapAuthentication
    {
        #region Variables

        private string path = System.Configuration.ConfigurationManager.AppSettings["LDAPServer"] + ":" + System.Configuration.ConfigurationManager.AppSettings["LDAPPort"];
        private string _filterAttribute;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"></param>
        public LdapAuthentication()
        {
        }

        #endregion

        /// <summary>
        /// Add the following IsAuthenticated method that accepts a domain name, user name and password as parameters and returns bool to indicate whether or not 
        /// the user with a matching password exists within Active Directory. The method initially attempts to bind to Active Directory using the supplied credentials. 
        /// If this is successful, the method uses the DirectorySearcher managed class to search for the specified user object. If located, the _path member is updated 
        /// to point to the user object and the _filterAttribute member is updated with the common name attribute of the user object. 
        /// </summary>
        /// <param name="domainAndUsername"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            //int testing = AuthenticateUser(username, pwd, out failure);
            string domainAndUsername = domain + @"\" + username;

            ArrayList pathArray = new ArrayList();
            pathArray.Add("LDAPS://ADOBENET.GLOBAL.ADOBE.COM");
            pathArray.Add("LDAPS://ADOBENET.GLOBAL.ADOBE.COM:636");
            pathArray.Add("LDAPS://ADOBENET.GLOBAL.ADOBE.COM:636/dc=adobe,dc=com");
            pathArray.Add("LDAPS://ADOBENET.GLOBAL.ADOBE.COM:636/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAPS://ADOBENET.GLOBAL.ADOBE.COM/dc=adobe,dc=com");
            pathArray.Add("LDAPS://ADOBENET.GLOBAL.ADOBE.COM/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAPS://NOI-GC.ADOBENET.GLOBAL.ADOBE.COM");
            pathArray.Add("LDAPS://NOI-GC.ADOBENET.GLOBAL.ADOBE.COM:636");
            pathArray.Add("LDAPS://NOI-GC.ADOBENET.GLOBAL.ADOBE.COM:636/dc=adobe,dc=com");
            pathArray.Add("LDAPS://NOI-GC.ADOBENET.GLOBAL.ADOBE.COM:636/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAPS://NOI-GC.ADOBENET.GLOBAL.ADOBE.COM/dc=adobe,dc=com");
            pathArray.Add("LDAPS://NOI-GC.ADOBENET.GLOBAL.ADOBE.COM/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAP://noildapr01.corp.adobe.com/dc=adobe,dc=com");
            pathArray.Add("LDAP://noildapr01.corp.adobe.com");
            pathArray.Add("LDAP://noildapr01.corp.adobe.com/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAP://noildapr01.corp.adobe.com:389/dc=adobe,dc=com");
            pathArray.Add("LDAP://noildapr01.corp.adobe.com:389");
            pathArray.Add("LDAP://noildapr01.corp.adobe.com:389/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAP://ADOBENET.GLOBAL.ADOBE.COM");
            pathArray.Add("LDAP://ADOBENET.GLOBAL.ADOBE.COM:389");
            pathArray.Add("LDAP://ADOBENET.GLOBAL.ADOBE.COM:389/dc=adobe,dc=com");
            pathArray.Add("LDAP://ADOBENET.GLOBAL.ADOBE.COM:839/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAP://ADOBENET.GLOBAL.ADOBE.COM/dc=adobe,dc=com");
            pathArray.Add("LDAP://ADOBENET.GLOBAL.ADOBE.COM/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAP://NOIADODC01.adobenet.global.adobe.com");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM:389");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM:389/dc=adobe,dc=com");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM:839/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM/dc=adobe,dc=com");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAP://NOIADODC01.adobenet.global.adobe.com");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM:389");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM:389/dc=adobe,dc=com");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM:839/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM/dc=adobe,dc=com");
            pathArray.Add("LDAP://NOIADODC01.ADOBENET.GLOBAL.ADOBE.COM/dc=adobenet,dc=global,dc=adobe,dc=com");

            pathArray.Add("ADOBENET.GLOBAL.ADOBE.COM");
            pathArray.Add("ADOBENET.GLOBAL.ADOBE.COM:389");
            pathArray.Add("ADOBENET.GLOBAL.ADOBE.COM:389/dc=adobe,dc=com");
            pathArray.Add("ADOBENET.GLOBAL.ADOBE.COM:389/dc=adobenet,dc=global,dc=adobe,dc=com");
            pathArray.Add("ADOBENET.GLOBAL.ADOBE.COM/dc=adobe,dc=com");
            pathArray.Add("ADOBENET.GLOBAL.ADOBE.COM/dc=adobenet,dc=global,dc=adobe,dc=com");

            DirectoryEntry entry = null;
            foreach (string path in pathArray)
            {
                entry = new DirectoryEntry(path, domainAndUsername, pwd);
                try
                {
                    Object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Log(ex, "User: " + domainAndUsername);
                }

                entry = new DirectoryEntry(path, domainAndUsername, pwd, AuthenticationTypes.Anonymous);
                try
                {
                    Object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Log(ex, "User: " + domainAndUsername);
                }

                entry = new DirectoryEntry(path, domainAndUsername, pwd, AuthenticationTypes.None);
                try
                {
                    Object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Log(ex, "User: " + domainAndUsername);
                }

                entry = new DirectoryEntry(path, domainAndUsername, pwd, AuthenticationTypes.Secure);
                try
                {
                    Object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Log(ex, "User: " + domainAndUsername);
                }

                entry = new DirectoryEntry(path, domainAndUsername, pwd, AuthenticationTypes.ReadonlyServer);
                try
                {
                    Object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Log(ex, "User: " + domainAndUsername);
                }
            }

            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                // Update the new path to the user in the directory
                path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                ExceptionLogger.Log(ex, "User: " + domainAndUsername);
                throw new Exception("Error authenticating user. " + ex.Message);
            }
            return true;
        }

        /// <summary>
        /// AuthenticateUser
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AuthenticateUser(string domain, string username, string password)
        {
            bool isSuccess = false;
            string domainAndUsername = domain + @"\" + username;

            LdapConnection obLdapConnection = null;
            NetworkCredential obNetworkCredential = null;
            LdapDirectoryIdentifier obLdapDirectoryIdentifier = null;

            try
            {
                //Response.Write("Binding");
                obNetworkCredential = new NetworkCredential(domainAndUsername, password);
                obLdapDirectoryIdentifier = new LdapDirectoryIdentifier(path);
                obLdapConnection = new LdapConnection(obLdapDirectoryIdentifier, obNetworkCredential, AuthType.Basic);
                obLdapConnection.Bind();
                isSuccess = true;
            }
            catch (System.DirectoryServices.Protocols.LdapException obException)
            {
                ExceptionLogger.Log(obException, "User: " + domainAndUsername);
                isSuccess = false;
            }

            finally
            {
                // Perform Cleanup
                obNetworkCredential = null;
                obLdapDirectoryIdentifier = null;
                if (null != obLdapConnection)
                {
                    obLdapConnection.Dispose();
                    obLdapConnection = null;
                }

            }

            return isSuccess;
        }

        /// <summary>
        /// This procedure extends the LdapAuthentication class to provide a GetGroups method, which will retrieve the list of groups that the current user is a member of. 
        /// The GetGroups method will return the group list as a pipe separated string, as in the following.
        /// "Group1|Group2|Group3|"
        /// </summary>
        /// <returns></returns>
        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();
            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount;
                     propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1),
                                      (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " +
                  ex.Message);
            }
            return groupNames.ToString();
        }
    }
}
