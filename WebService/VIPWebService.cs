using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Partner;
using WebService.WSUpdateSobject;

namespace WebService
{
    class VIPWebService
    {
        static void Main(string[] args)
        {
            //Authenticating and Creating a Session

            String strApiUrl = (String)WebService.Properties.Settings.Default.ApiUrl;//Create the Endpoint.
            String strUsername = (String)WebService.Properties.Settings.Default.Username;//Set the instance username. [Set Username and password in App.config]
            String strPassword = (String)WebService.Properties.Settings.Default.Password;//Set password with combination of password and security token.
            SforceService sforceService = new SforceService();//Create Partner Wsdl class SforceService object.

            System.Console.WriteLine("Processing....");

            sforceService.Url = strApiUrl;//Set the Endpoint.

            LoginResult loginResult = sforceService.login(strUsername, strPassword);//loginResult object. 

            if (loginResult != null && !loginResult.passwordExpired)
            {
                //Consuming Force.com webservice
  
                WSUpdateSobject.WSUpdateSObjectsService WsUpdaService = new WSUpdateSObjectsService();//Create Webservice instance.
                WSUpdateSobject.SessionHeader header = new WSUpdateSobject.SessionHeader();//Create session header instance.
                header.sessionId = loginResult.sessionId;//Set sessionId in header.
                WsUpdaService.SessionHeaderValue = header;//Set header in Webservice. 


                List<WSUpdateSobject.LoanOpprtntyDetail> lstLoanOpp = new List<WSUpdateSobject.LoanOpprtntyDetail>();//Create list of LoanOpprtntyDetail.
                WSUpdateSobject.LoanOpprtntyDetail objLoan = new WSUpdateSobject.LoanOpprtntyDetail();
                objLoan.OpportunityID = "006e0000007ol9L";
                objLoan.Lender = "Lender123";
                lstLoanOpp.Add(objLoan);

                List<WSUpdateSobject.ContactDetail> lstObjcnt = new List<WSUpdateSobject.ContactDetail>();//Create list of ContactDetail.
                WSUpdateSobject.ContactDetail objcnt = new WSUpdateSobject.ContactDetail();
                objcnt.ContactID = "003e000000MSycH";
                objcnt.BorrowerFirstName = "Milan";
                lstObjcnt.Add(objcnt);

                String message = WsUpdaService.UpdtCntctNLnOpprtntyDetail(lstLoanOpp.ToArray(), lstObjcnt.ToArray());//Call the webservice method UpdtCntctNLnOpprtntyDetail.
                System.Console.WriteLine("Result---." + message);
                Console.ReadLine();
            }

        }
    }
}
