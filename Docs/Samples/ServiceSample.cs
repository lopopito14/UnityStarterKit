namespace Samples
{
    using UnityStarterKit.Modularity;
    using UnityStarterKit.Threading.Services;

    public class IServiceSample : IServiceModule
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is service running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is service running; otherwise, <c>false</c>.
        /// </value>
        bool IsServiceRunning { get; }

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        void StartMonitoring();

        /// <summary>
        /// Stops the monitoring.
        /// </summary>
        void StopMonitoring();
    }

    public class ServiceSample : ServiceModule, IServiceSample
    {
#pragma warning disable 0649
        [InjectableService]
        private IMainThreadService mainThreadService;
#pragma warning restore 0649

        private bool isServiceRunning;
        private readonly string name;
        /// <summary>
        /// CancellationTokenSource for cancel the main routine.
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        public ServiceSample() : this("ServiceSample") { }

        public ServiceSample(string name)
        {
            this.name = name;
            isServiceRunning = false;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string IServiceSample.Name => name;

        /// <summary>
        /// Gets a value indicating whether this instance is service running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is service running; otherwise, <c>false</c>.
        /// </value>
        bool IServiceSample.IsServiceRunning => isServiceRunning;

        /// <summary>
        /// Starts the monitoring.
        /// </summary>
        void IServiceSample.StartMonitoring()
        {
            if (isServiceRunning) return;

            StartMonitoring();
            isServiceRunning = true;
        }

        /// <summary>
        /// Stops the monitoring.
        /// </summary>
        void IServiceSample.StopMonitoring()
        {
            if (!isServiceRunning) return;

            StopMonitoring();
            isServiceRunning = false;
        }

        /// <summary>
        /// Run in another task to prevent UI thread blocking.
        /// </summary>
        private void StartMonitoring()
        {
            Task.Run(async () =>
            {
                logger.Info("Start Monitoring");

                using (cancellationTokenSource = new CancellationTokenSource())
                {
                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        SoapExecuteOutput<SoapResponseSample> soapResponseSample = await SoapExecuteSample.Execute(new SoapExecuteInput(WebServiceAddress, 50), $"{name} - payload");

                        if (soapResponseSample != null)
                        {
                            string payload = soapResponseSample.payload;

                            mainThreadService.DispatchActionOnMainThread(() =>
                            {
                                // do stuff on UI thread with the payload
                            });

                            logger.Info($"Response from the WS => {payload}");
                        }
                    }
                }

                logger.Info("Stop Monitoring");
            });
        }

        /// <summary>
        /// Stop the main routine by cancelling the cancellationTokenSource.
        /// </summary>
        private void StopMonitoring()
        {
            cancellationTokenSource.Cancel();
        }
    }
}