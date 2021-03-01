namespace Samples
{
    using System;
    using System.Threading.Tasks;
    using UnityStarterKit.Web.Soap;

    public sealed class SoapExecuteSample : SoapExecute<SoapResquestSample, SoapResponseSample>
    {
        protected override Type RequestEnvelopeType => typeof(SoapRequestEnvelopeSample);

        protected override Type ResponseEnvelopeType => typeof(SoapResponseEnvelopeSample);

        protected override ISoapRequestEnvelope BuildRequest(SoapResquestSample request)
        {
            return new SoapRequestEnvelopeSample
            {
                Body = new SoapRequestBodySample
                {
                    SoapResquestSample = request
                }
            };
        }
        
        protected override SoapResponseSample BuildResponse(ISoapResponseEnvelope soapResponseEnvelope)
        {
            if (soapResponseEnvelope is SoapResponseEnvelopeSample soapResponseEnvelopeSample)
            {
                if (soapResponseEnvelopeSample != null && soapResponseEnvelopeSample.Body != null)
                {
                    return soapResponseEnvelopeSample.Body.SoapResponseSample;
                }

                return null;
            }

            throw new InvalidCastException();
        }

        public static Task<SoapExecuteOutput<SoapResponseSample>> Execute(SoapExecuteInput soapExecuteInput, string input)
        {
            return new SoapExecuteSample()._Execute_(
                new SoapExecuteInput<SoapResquestSample>(
                    soapExecuteInput,
                    new SoapResquestSample
                    {
                        payload = input
                    }
                )
            );
        }

    }
}