using System.Collections.Generic;
using Auto.Data.Entities;
using DefaultNamespace;

namespace Auto.Data {
	public interface IAutoDatabase {
		
		public int CountVehicles();
		public int CountAutoOwner();
		public IEnumerable<Vehicle> ListVehicles();
		public IEnumerable<AutoOwner> ListAutoOwners();
		public IEnumerable<Manufacturer> ListManufacturers();
		public IEnumerable<Model> ListModels();

		public Vehicle FindVehicle(string registration);
		public AutoOwner FindAutoOwner(string number);
		public Model FindModel(string code);
		public Manufacturer FindManufacturer(string code);

		public void CreateVehicle(Vehicle vehicle);
		public void UpdateVehicle(Vehicle vehicle);
		public void DeleteVehicle(Vehicle vehicle);

		public void CreateAutoOwner(AutoOwner autoOwner);
		public void UpdateAutoOwner(AutoOwner autoOwner);
		public void DeleteAutoOwner(AutoOwner autoOwner);
	}
}
