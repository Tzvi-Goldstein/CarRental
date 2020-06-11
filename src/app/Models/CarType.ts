export class CarType {
  constructor(
  public Manufacturer?: string,
  public Model?: string,
  public CostPerDay?: number,
  public DelayCostPerDay?: number,
  public YearManufactured?: Date,
  public IsGear?: boolean,
  public ModelId?: number
  ) {}
}
 