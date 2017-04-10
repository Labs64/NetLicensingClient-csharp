
namespace NetLicensingDemo
{
    using System;
    using System.Web;
    using System.Web.UI;

    public partial class Default : System.Web.UI.Page
    {
        protected LicensingInfo licensingInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            licensingInfo = LicensingInfo.getLicensingInfo();
            licensingInfo.licenseeNumber = textLicenseeNumber.Text;
            licensingInfo.sourceLicenseeNumber = textSourceLicenseeNumber.Text;
        }

        public void buttonGetLicenseeNumberClicked(object sender, EventArgs args)
        {
            textLicenseeNumber.Text = "licensee-" + HardwareFingerprint.GetHash(textHardwareId.Text);
        }

        public void textLicenseeNumberChanged(object sender, EventArgs args) {
            licensingInfo.licenseeNumber = textLicenseeNumber.Text;
        }

        public void buttonValidateClicked(object sender, EventArgs args)
        {
            licensingInfo.updateLicensingInfo();
            if (licensingInfo.module1.isValid)
            {
                if (licensingInfo.module1.isEvaluation)
                {
                    textTryBuy.Text = "evaluation until " + licensingInfo.module1.evaluationExpires;
                }
                else
                {
                    textTryBuy.Text = "full functionality available";
                }
            }
            else
            {
                textTryBuy.Text = "disabled, evaluation expired";
            }
            if (licensingInfo.module2.isValid)
            {
                textSubscription.Text = "subscribed until " + licensingInfo.module2.expires;
            }
            else
            {
                textSubscription.Text = "disabled, subscription expired";
            }
            message.Text = licensingInfo.errorInfo;
        }

        public void textSourceLicenseeNumberChanged (object sender, EventArgs args)
        {
	        licensingInfo.sourceLicenseeNumber = textSourceLicenseeNumber.Text;
        }

        public void buttonTransferClicked (object sender, EventArgs args)
        {
        	licensingInfo.transferLicenses();
            message.Text = licensingInfo.errorInfo;

        }

    }
}
