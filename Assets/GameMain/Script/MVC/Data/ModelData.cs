using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelData  {

    public string no;
    public string x;
    public string y;
    public string z;
    public string volume;
    public string p;
    public string voltage;
    public string kg;
    public string output;//产量
    public string fuel_gas_haoliang;//燃气耗量
    public string fuel_gas_caliber; //燃气口径
    public string water_caliber;    //给口水径
    public int outputNumber;
    public ModelData(string no,string x,string y,string z,string volume,string p,string voltage,string kg,string output,string fuel_gas_haoliang,string fuel_gas_caliber,string water_caliber)
    {
        this.no = no;
        this.x = x;
        this.y = y;
        this.z = z;
        this.volume = volume;
        this.p = p;
        this.voltage = voltage;
        this.kg = kg;
        this.output = output;
        this.fuel_gas_haoliang = fuel_gas_haoliang;
        this.fuel_gas_caliber = fuel_gas_caliber;
        this.water_caliber = water_caliber;
    }
}