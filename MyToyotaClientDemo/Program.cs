using MyToyotaClient;

namespace MyToyotaClientDemo;

internal class Program
{
    static void Main(string[] args)
    {
        // replace this with your credentials!
        // please remind not pushing code changes with your credentials!
        var username = System.IO.File.ReadAllText(@"C:\Credentials\ToyotaClientUsername.txt");
        var password = System.IO.File.ReadAllText(@"C:\Credentials\ToyotaClientPassword.txt");

        var client = new MyToyota()
            .UseCredentials(username, password)
            .UseLogger( (string message) => Console.WriteLine(message))
            .UseTimeout(30);

        Console.WriteLine("Login to Toyota Connected Services API...");
        client.Login();
        Console.WriteLine("");


        var vehicles = client.GetVehicles();
        if (vehicles == null || !vehicles.payload.Any())
            return;


        foreach(var vehicle in vehicles.payload)
        {
            Console.WriteLine($"--------------------- vehicle {vehicle.vin}: ---------------------");
            Console.WriteLine($"Car line         : {vehicle.carlineName}");
            Console.WriteLine($"Color            : {vehicle.color}");
            Console.WriteLine($"chassis number   : {vehicle.vin}");
            Console.WriteLine($"IMEI             : {vehicle.imei}");
            Console.WriteLine($"");


            var electric = client.GetElectric(vehicle.vin);
            if (electric is not null && electric.payload is not null)
            {
                Console.WriteLine($"Electric info:");
                Console.WriteLine($"Battery level    : {electric.payload.batteryLevel} %");
                Console.WriteLine($"Range            : {electric.payload.evRange.value:N0} {electric.payload.evRange.unit}");
                Console.WriteLine($"Range with AC    : {electric.payload.evRangeWithAc.value:N0} {electric.payload.evRangeWithAc.unit}");
                Console.WriteLine($"");
            }


            var location = client.GetLocation(vehicle.vin);
            if (location is not null && location.payload is not null)
            {
                Console.WriteLine($"Location info:");
                Console.WriteLine($"Status           : {location.status}");
                Console.WriteLine($"Code             : {location.code}");
                Console.WriteLine($"Location         : {location.payload.vehicleLocation.latitude} / {location.payload.vehicleLocation.longitude}");
                Console.WriteLine($"Display name     : {location.payload.vehicleLocation.displayName}");
                Console.WriteLine($"Acquisition time : {location.payload.vehicleLocation.locationAcquisitionDatetime}");
                Console.WriteLine($"");
            }


            var health = client.GetHealthStatus(vehicle.vin);
            if (health is not null && health.payload is not null)
            {
                Console.WriteLine($"Health status:");
                Console.WriteLine($"Oil data         : {health.payload.quantityOfEngOilIcon.Count} entries");
                Console.WriteLine($"Warnings         : {health.payload.warning.Count} entries");
                Console.WriteLine($"Last update      : {health.payload.wnglastUpdTime}");
                Console.WriteLine($"");
            }


            var telemetry = client.GetTelemetryStatus(vehicle.vin);
            if (telemetry is not null && telemetry.payload is not null)
            {
                Console.WriteLine($"Telemetry status:");
                Console.WriteLine($"Distance to empty: {telemetry.payload.distanceToEmpty.value} {telemetry.payload.distanceToEmpty.unit}");
                Console.WriteLine($"Odometer         : {telemetry.payload.odometer.value} {telemetry.payload.odometer.unit}");
                Console.WriteLine($"Timestamp        : {telemetry.payload.timestamp}");
                Console.WriteLine($"");
            }


            var remote = client.GetRemoteStatus(vehicle.vin);
            if (remote is not null && remote.payload is not null)
            {
                Console.WriteLine($"Remote status:");
                Console.WriteLine($"Position         : {remote.payload.latitude} / {remote.payload.longitude}");
                Console.WriteLine($"Position time    : {remote.payload.locationAcquisitionDatetime}");
                Console.WriteLine($"Occurrence date  : {remote.payload.occurrenceDate}");
                Console.WriteLine($"Fug age          : {remote.payload.telemetry.fugage.value} {remote.payload.telemetry.fugage.unit}");
                Console.WriteLine($"Odo              : {remote.payload.telemetry.odo.value} {remote.payload.telemetry.odo.unit}");
                Console.WriteLine($"Rage             : {remote.payload.telemetry.rage.value} {remote.payload.telemetry.rage.unit}");
                Console.WriteLine($"vehicle status:");
                foreach (var status in remote.payload.vehicleStatus)
                    foreach(var s in status.sections)
                        Console.WriteLine($"    {s.section.Replace("carstatus_item_", ""),-30}: {(string.Join(',', s.values.Select(v => v.value.Replace("carstatus_", ""))))}");
                Console.WriteLine($"");
            }


            var service = client.GetServiceHistory(vehicle.vin);
            if (service is not null && service.payload is not null)
            {
                Console.WriteLine($"Service events:");
                foreach(var ev in service.payload.serviceHistories.OrderBy(h => h.serviceDate))
                    Console.WriteLine($"    {ev.serviceDate}     : {ev.mileage,6} {ev.unit,-2}:   {ev.serviceCategory} - {ev.serviceProvider} (ID {ev.serviceHistoryId})");
                Console.WriteLine($"");
            }
        }
    }
}
