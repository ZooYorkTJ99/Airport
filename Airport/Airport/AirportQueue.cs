using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    /*
     * [Assumptions]
     * 
     * Data will be in Json format.
     * When using class, data will be valid and free of errors.
     * When using class, requests will be in format "small" or "large" free of errors.
     * When 0 planes are a result of the requested runway, null value is an acceptable return.
     * Large planes must use large runways, but Small planes can use either runway, will be based on FIFO.
     */
    
    public class AirportQueue
    {
        List<Plane> listPlane = new List<Plane>();
        public void LoadQueue(string queueData)
        {
            listPlane = JsonConvert.DeserializeObject<List<Plane>>(queueData);
        }

        public Plane NextPlane(string runwaySize)
        {
            Plane currPlane = null;
            runwaySize = runwaySize.ToUpper();
            
            foreach(Plane plane in listPlane)
            {
                string size = plane.size.ToUpper();
                string id = plane.id.ToUpper();
                string type = plane.type.ToUpper();

                if(runwaySize == "SMALL")
                {
                    if(size == "SMALL" && type == "PASSENGER")
                    {
                        listPlane.Remove(plane);
                        return plane;
                    }
                    else if(size == "SMALL" && currPlane == null)
                    {
                        currPlane = plane;
                    }
                }
                else if(runwaySize == "LARGE")
                {
                    if (type == "PASSENGER")
                    {
                        listPlane.Remove(plane);
                        return plane;
                    }
                    else if (currPlane == null)
                    {
                        currPlane = plane;
                    }
                }
            }
            listPlane.Remove(currPlane);
            return currPlane;
        } 
    }
}


