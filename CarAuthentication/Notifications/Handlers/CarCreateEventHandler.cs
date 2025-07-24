using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarAuthentication.Notifications.Handlers
{
    // File: Notifications/Handlers/CarCreatedEventHandler.cs
  

   
        public class CarCreatedEventHandler : INotificationHandler<CarCreatedEvent>
        {
            private readonly ILogger<CarCreatedEventHandler> _logger;
            public CarCreatedEventHandler(ILogger<CarCreatedEventHandler> logger)
            {
                _logger = logger;
            }
            public Task Handle(CarCreatedEvent notification, CancellationToken cancellationToken)
            {
                _logger.LogInformation(" [EVENT FIRED] New car created:");
                _logger.LogInformation("Car ID: {CarId}, Brand: {Brand}, Model: {Model}, Year: {Year}, Place: {Place}",
                    notification.CarId,
                    notification.Brand,
                    notification.Model,
                    notification.Year,
                    notification.Place);
                Console.WriteLine("🚘 [EVENT FIRED] A new car has been created:");
                Console.WriteLine($"   ID     : {notification.CarId}");
                Console.WriteLine($"   Brand  : {notification.Brand}");
                Console.WriteLine($"   Model  : {notification.Model}");
                Console.WriteLine($"   Year   : {notification.Year}");
                Console.WriteLine($"   Place  : {notification.Place}");
             

                return Task.CompletedTask;
            }
        }
    }


