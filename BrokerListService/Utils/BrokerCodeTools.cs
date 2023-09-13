namespace BrokerListService.Utils
{
    public class BrokerCodeTools
    {
        public static string GetHeadCode(string brokerCode)
        {
            var headCode = "";
            if (brokerCode[0] == '9')
            {
                headCode = brokerCode[..2];
            }
            else
            {
                headCode = brokerCode[..3];
            }
            return headCode;
        }
    }
}
