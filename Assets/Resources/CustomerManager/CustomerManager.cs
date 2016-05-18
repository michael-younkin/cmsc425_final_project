using UnityEngine;
using System.Collections;

public class CustomerManager : MonoBehaviour {

    public CustomerController customerPrefab;
    public BurritoController burritoPrefab;
    private CustomerController customer;
    private Waypoint enterWaypoint;

	// Use this for initialization
	void Start () {
        enterWaypoint = GameUtil.SafeFind("EnterWaypoint").SafeGetComponent<Waypoint>();
	}

    private void Customer_ReachedDestination(CustomerController customer, Waypoint dest, string dest_id)
    {
        if (dest_id.Equals("Cashier"))
        {
            // Create a new customer
            customer = createCustomer();
        }
    }

    private CustomerController createCustomer()
    {
        CustomerController c = Instantiate<CustomerController>(customerPrefab);
        c.transform.position = enterWaypoint.position;
        c.ReachedDestination += Customer_ReachedDestination;

        // Create a burrito along with the customer
        BurritoController burrito = Instantiate<BurritoController>(burritoPrefab);
        Waypoint burritoStart = GameUtil.SafeFind("BurritoEnterWaypoint").SafeGetComponent<Waypoint>();
        float burritoX = GameUtil.SafeFind("Worker").transform.position.x;
        burrito.transform.position.Set(burritoX, burritoStart.position.y, burritoStart.position.z);

        return c;
    }

    // Update is called once per frame
    void Update () {
	}
}
