namespace Auto.Messages
{
    public class NewVehiclePriceMessage : NewVehicleMessage
    {
        public int Price { get; set; }
        public string CurrencyCode{ get; set; }

        public NewVehiclePriceMessage()
        {
            
        }

        public NewVehiclePriceMessage(NewVehicleMessage vehicle, int price, string currencyCode)
        {
            this.Registration= vehicle.Registration;
            this.Color= vehicle.Color;
            this.Year= vehicle.Year;
            this.ManufacturerName= vehicle.ManufacturerName;
            this.ModelName= vehicle.ModelName;
            this.Price= price;
            this.CurrencyCode= currencyCode;
        }
    }
}