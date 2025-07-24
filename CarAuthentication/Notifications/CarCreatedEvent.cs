
using MediatR;

namespace CarAuthentication.Notifications
{
    // File: Notifications/CarCreatedEvent.cs
  

   
        public class CarCreatedEvent : INotification
        {
            public string CarId { get; set; } = null!;
            public string UserId { get; set; } = null!;
            public string Brand { get; set; } = null!;
            public string Model { get; set; } = null!;
            public int Year { get; set; }
            public string Place { get; set; } = null!;
        }
    

}
