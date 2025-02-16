using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyToyotaClient.Models;

public class Capability
{
    public string name { get; set; }
    public object description { get; set; }
    public bool value { get; set; }
    public Translation translation { get; set; }
    public bool display { get; set; }
    public object displayName { get; set; }
}

public class CtsLinks
{
    public string name { get; set; }
    public string link { get; set; }
    public object imageUrl { get; set; }
    public object body { get; set; }
    public object buttonText { get; set; }
}

public class DataConsent
{
    public string serviceConnect { get; set; }
    public string can300 { get; set; }
    public string dealerContact { get; set; }
    public string ubi { get; set; }
}

public class Dcm
{
    public string dcmModelYear { get; set; }
    public string dcmDestination { get; set; }
    public string countryCode { get; set; }
    public string dcmSupplier { get; set; }
    public string dcmSupplierName { get; set; }
    public string dcmGrade { get; set; }
    public string euiccid { get; set; }
    public object hardwareType { get; set; }
    public object vehicleUnitTerminalNumber { get; set; }
}

public class DisplaySubscription
{
    public string productName { get; set; }
    public string displayStatus { get; set; }
}

public class ExtendedCapabilities
{
    public bool remoteEngineStartStop { get; set; }
    public bool remoteEConnectCapable { get; set; }
    public bool doorLockUnlockCapable { get; set; }
    public bool frontDriverDoorLockStatus { get; set; }
    public bool frontPassengerDoorLockStatus { get; set; }
    public bool rearDriverDoorLockStatus { get; set; }
    public bool rearPassengerDoorLockStatus { get; set; }
    public bool frontDriverDoorOpenStatus { get; set; }
    public bool frontPassengerDoorOpenStatus { get; set; }
    public bool rearDriverDoorOpenStatus { get; set; }
    public bool rearPassengerDoorOpenStatus { get; set; }
    public bool frontDriverDoorWindowStatus { get; set; }
    public bool frontPassengerDoorWindowStatus { get; set; }
    public bool rearDriverDoorWindowStatus { get; set; }
    public bool rearPassengerDoorWindowStatus { get; set; }
    public bool rearHatchRearWindow { get; set; }
    public bool moonroof { get; set; }
    public bool powerWindowsCapable { get; set; }
    public bool hazardCapable { get; set; }
    public bool hornCapable { get; set; }
    public bool lightsCapable { get; set; }
    public bool climateCapable { get; set; }
    public bool climateTemperatureControlFull { get; set; }
    public bool climateTemperatureControlLimited { get; set; }
    public bool frontDriverSeatHeater { get; set; }
    public bool frontPassengerSeatHeater { get; set; }
    public bool rearDriverSeatHeater { get; set; }
    public bool rearPassengerSeatHeater { get; set; }
    public bool frontDriverSeatVentilation { get; set; }
    public bool frontPassengerSeatVentilation { get; set; }
    public bool rearDriverSeatVentilation { get; set; }
    public bool rearPassengerSeatVentilation { get; set; }
    public bool steeringHeater { get; set; }
    public bool mirrorHeater { get; set; }
    public bool frontDefogger { get; set; }
    public bool rearDefogger { get; set; }
    public bool vehicleFinder { get; set; }
    public bool guestDriver { get; set; }
    public bool buzzerCapable { get; set; }
    public bool trunkLockUnlockCapable { get; set; }
    public bool evChargeStationsCapable { get; set; }
    public bool fcvStationsCapable { get; set; }
    public bool lastParkedCapable { get; set; }
    public bool acScheduling { get; set; }
    public bool chargeManagement { get; set; }
    public bool nextCharge { get; set; }
    public bool weeklyCharge { get; set; }
    public bool dailyCharge { get; set; }
    public bool powerTailgateCapable { get; set; }
    public bool batteryStatus { get; set; }
    public bool evBattery { get; set; }
    public bool drivePulse { get; set; }
    public bool electricPulse { get; set; }
    public bool hydrogenPulse { get; set; }
    public bool hybridPulse { get; set; }
    public bool emergencyAssist { get; set; }
    public bool bumpCollisions { get; set; }
    public bool fuelLevelAvailable { get; set; }
    public bool fuelRangeAvailable { get; set; }
    public bool equippedWithAlarm { get; set; }
    public bool manualRearWindows { get; set; }
    public bool lightStatus { get; set; }
    public bool bonnetStatus { get; set; }
    public bool sunroof { get; set; }
    public bool smartKeyStatus { get; set; }
    public bool ecare { get; set; }
    public bool tryAndPlay { get; set; }
    public bool dashboardWarningLights { get; set; }
    public bool weHybridCapable { get; set; }
    public bool enhancedSecuritySystemCapable { get; set; }
    public bool vehicleStatus { get; set; }
    public bool stellantisVehicleStatusCapable { get; set; }
    public bool stellantisClimateCapable { get; set; }
    public bool vehicleDiagnosticCapable { get; set; }
    public bool telemetryCapable { get; set; }
    public bool econnectClimateCapable { get; set; }
    public bool econnectVehicleStatusCapable { get; set; }
}

public class Features
{
    public int achPayment { get; set; }
    public int addServiceRecord { get; set; }
    public int autoDrive { get; set; }
    public int cerence { get; set; }
    public int chargingStation { get; set; }
    public int climateStartEngine { get; set; }
    public int collisionAssistance { get; set; }
    public int connectedCard { get; set; }
    public int connectedInsurance { get; set; }
    public int connectedSupport { get; set; }
    public int crashNotification { get; set; }
    public int criticalAlert { get; set; }
    public int dashboardLights { get; set; }
    public int dealerAppointment { get; set; }
    public int digitalKey { get; set; }
    public int doorLockCapable { get; set; }
    public int drivePulse { get; set; }
    public int driverCompanion { get; set; }
    public int driverScore { get; set; }
    public int dtcAccess { get; set; }
    public int dynamicNavi { get; set; }
    public int ecoHistory { get; set; }
    public int ecoRanking { get; set; }
    public int electricPulse { get; set; }
    public int emergencyAssist { get; set; }
    public int enhancedSecuritySystem { get; set; }
    public int evChargeStation { get; set; }
    public int evRemoteServices { get; set; }
    public int evVehicleStatus { get; set; }
    public int financialServices { get; set; }
    public int flexRental { get; set; }
    public int h2FuelStation { get; set; }
    public int homeCharge { get; set; }
    public int howToVideos { get; set; }
    public int hybridPulse { get; set; }
    public int hydrogenPulse { get; set; }
    public int importantMessage { get; set; }
    public int insurance { get; set; }
    public int lastParked { get; set; }
    public int lcfs { get; set; }
    public int linkedAccounts { get; set; }
    public int maintenanceTimeline { get; set; }
    public int marketingCard { get; set; }
    public int marketingConsent { get; set; }
    public int masterConsentEditable { get; set; }
    public int myDestination { get; set; }
    public int ownersManual { get; set; }
    public int paidProduct { get; set; }
    public int parkedVehicleLocator { get; set; }
    public int parking { get; set; }
    public int parkingNotes { get; set; }
    public int personalizedSettings { get; set; }
    public int privacy { get; set; }
    public int recentTrip { get; set; }
    public int remoteDtc { get; set; }
    public int remoteParking { get; set; }
    public int remoteService { get; set; }
    public int roadsideAssistance { get; set; }
    public int safetyRecall { get; set; }
    public int scheduleMaintenance { get; set; }
    public int sendToCar { get; set; }
    public int serviceHistory { get; set; }
    public int shopGenuineParts { get; set; }
    public int smartCharging { get; set; }
    public int ssaDownload { get; set; }
    public int sxmRadio { get; set; }
    public int telemetry { get; set; }
    public int tff { get; set; }
    public int tirePressure { get; set; }
    public int v1g { get; set; }
    public int vaSetting { get; set; }
    public int vehicleDiagnostic { get; set; }
    public int vehicleHealthReport { get; set; }
    public int vehicleSpecifications { get; set; }
    public int vehicleStatus { get; set; }
    public int weHybrid { get; set; }
    public int wifi { get; set; }
    public int xcapp { get; set; }
}

public class HeadUnit
{
    public string mobilePlatformCode { get; set; }
    public string huDescription { get; set; }
    public string huGeneration { get; set; }
    public string huVersion { get; set; }
    public string multimediaType { get; set; }
    public string deviceId { get; set; }
}

public class Message
{
    public string responseCode { get; set; }
    public string description { get; set; }
    public string detailedDescription { get; set; }
}

public class Vehicle
{
    public object registrationNumber { get; set; }
    public string vin { get; set; }
    public string modelYear { get; set; }
    public string modelName { get; set; }
    public string modelDescription { get; set; }
    public string modelCode { get; set; }
    public string region { get; set; }
    public string status { get; set; }
    public string generation { get; set; }
    public string image { get; set; }
    public string nickName { get; set; }
    public string brand { get; set; }
    public object hwType { get; set; }
    public string asiCode { get; set; }
    public string subscriptionStatus { get; set; }
    public bool primarySubscriber { get; set; }
    public bool remoteUser { get; set; }
    public bool evVehicle { get; set; }
    public string remoteSubscriptionStatus { get; set; }
    public string remoteUserGuid { get; set; }
    public string subscriberGuid { get; set; }
    public string remoteDisplay { get; set; }
    public object emergencyContact { get; set; }
    public RemoteServiceCapabilities remoteServiceCapabilities { get; set; }
    public bool nonCvtVehicle { get; set; }
    public List<object> remoteServicesExceptions { get; set; }
    public ExtendedCapabilities extendedCapabilities { get; set; }
    public string electricalPlatformCode { get; set; }
    public PersonalizedSettings personalizedSettings { get; set; }
    public string fuelType { get; set; }
    public object fleetInd { get; set; }
    public bool svlStatus { get; set; }
    public string displayModelDescription { get; set; }
    public string manufacturerCode { get; set; }
    public object suffixCode { get; set; }
    public string imei { get; set; }
    public string color { get; set; }
    public object vehicleDataConsents { get; set; }
    public DataConsent dataConsent { get; set; }
    public List<object> vehicleCapabilities { get; set; }
    public List<Capability> capabilities { get; set; }
    public List<Subscription> subscriptions { get; set; }
    public List<object> services { get; set; }
    public List<DisplaySubscription> displaySubscriptions { get; set; }
    public List<object> alerts { get; set; }
    public Features features { get; set; }
    public bool subscriptionExpirationStatus { get; set; }
    public string faqUrl { get; set; }
    public object shopGenuinePartsUrl { get; set; }
    public bool familySharing { get; set; }
    public CtsLinks ctsLinks { get; set; }
    public TffLinks tffLinks { get; set; }
    public HeadUnit headUnit { get; set; }
    public string katashikiCode { get; set; }
    public object oldImei { get; set; }
    public string stockPicReference { get; set; }
    public string transmissionType { get; set; }
    public string dateOfFirstUse { get; set; }
    public Dcm dcm { get; set; }
    public List<object> dcms { get; set; }
    public string manufacturedDate { get; set; }
    public string contractId { get; set; }
    public string carlineName { get; set; }
    public object externalSubscriptions { get; set; }
    public object serviceConnectStatus { get; set; }
    public object proXSeatsVideoLink { get; set; }
    public bool owner { get; set; }
    public bool commercialRental { get; set; }
    public bool remoteSubscriptionExists { get; set; }
    public bool dcmActive { get; set; }
    public int preferred { get; set; }
}

public class PersonalizedSettings
{
    public object name { get; set; }
    public object link { get; set; }
    public object imageUrl { get; set; }
    public object body { get; set; }
    public object buttonText { get; set; }
}

public class RemoteServiceCapabilities
{
    public bool guestDriverCapable { get; set; }
    public bool vehicleFinderCapable { get; set; }
    public bool estartStopCapable { get; set; }
    public bool estartEnabled { get; set; }
    public bool estopEnabled { get; set; }
    public bool hazardCapable { get; set; }
    public bool dlockUnlockCapable { get; set; }
    public bool headLightCapable { get; set; }
    public bool acsettingEnabled { get; set; }
    public bool trunkCapable { get; set; }
    public bool powerWindowCapable { get; set; }
    public bool ventilatorCapable { get; set; }
    public bool steeringWheelHeaterCapable { get; set; }
    public bool allowHvacOverrideCapable { get; set; }
    public bool moonRoofCapable { get; set; }
}

public class Status
{
    public List<Message> messages { get; set; }
}

public class VehiclesModel
{
    [JsonPropertyName("status")]
    public Status status { get; set; }
    [JsonPropertyName("payload")]
    public List<Vehicle> payload { get; set; }
}

public class Subscription
{
    public string subscriptionID { get; set; }
    public string productName { get; set; }
    public string productDescription { get; set; }
    public string displayProductName { get; set; }
    public string subscriptionEndDate { get; set; }
    public object subscriptionNextBillingDate { get; set; }
    public string subscriptionStartDate { get; set; }
    public string status { get; set; }
    public string type { get; set; }
    public int subscriptionRemainingDays { get; set; }
    public object subscriptionRemainingTerm { get; set; }
    public string productCode { get; set; }
    public string productLine { get; set; }
    public object productType { get; set; }
    public int term { get; set; }
    public string termUnit { get; set; }
    public object goodwillIssuedFor { get; set; }
    public bool renewable { get; set; }
    public string displayTerm { get; set; }
    public string subscriptionTerm { get; set; }
    public bool autoRenew { get; set; }
    public bool futureCancel { get; set; }
    public List<object> consolidatedProductIds { get; set; }
    public List<object> consolidatedGoodwillIds { get; set; }
    public object components { get; set; }
    public string category { get; set; }
}

public class TffLinks
{
    public object name { get; set; }
    public object link { get; set; }
    public object imageUrl { get; set; }
    public object body { get; set; }
    public object buttonText { get; set; }
}

public class Translation
{
    public object english { get; set; }
    public object french { get; set; }
    public object spanish { get; set; }
}
