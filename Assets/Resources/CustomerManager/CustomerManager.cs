using UnityEngine;
using System.Collections;

public class CustomerManager : MonoBehaviour {

    public CustomerController customerPrefab;
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
        return c;
    }

    // Update is called once per frame
    void Update () {
	}
}
