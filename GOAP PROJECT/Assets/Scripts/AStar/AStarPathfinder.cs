using UnityEngine;
using Pathfinding;
using System.IO;
using System.Diagnostics;

public class AStarPathfinder : MonoBehaviour
{
    private Seeker seeker;                       // Reference to the Seeker component
    private Pathfinding.Path currentPath;         // Reference to the current calculated path
    private int currentWaypointIndex;             // Index of the current waypoint in the path

    public float speed = 5f;                      // Speed of the character
    public float waypointReachedThreshold = 0.2f; // Distance threshold to consider a waypoint reached
    private Transform spriteObject; // Reference to the child object containing the sprite renderer


    private void Start()
    {
        // Get the child object with the sprite renderer component
        spriteObject = transform.GetChild(0);
        seeker = GetComponent<Seeker>();
    }

    // Method to start calculating a new path
    public void CalculatePath(Vector3 targetPosition)
    {
        seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        UnityEngine.Debug.Log("Calculating path to target position: " + targetPosition);
    }

    private void OnPathComplete(Pathfinding.Path p)
    {
        ABPath path = p as ABPath;
        if (path != null && !path.error)
        {
            currentPath = path;
            currentWaypointIndex = 0;
        }
    }

    private void MoveAlongPath()
    {
        if (currentPath == null)
        {
            // No path available
            return;
        }

        if (currentWaypointIndex >= currentPath.vectorPath.Count)
        {
            // Reached the end of the path
            return;
        }

        // Move towards the current waypoint without rotation
        Vector3 direction = (currentPath.vectorPath[currentWaypointIndex] - transform.position).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;
        movement.z = 0f; // Set the z-coordinate to 0 to prevent rotation
        transform.Translate(movement, Space.World); // Use Space.World to ensure the movement is in world space coordinates

        // Rotate the parent object to face the current waypoint
        transform.LookAt(currentPath.vectorPath[currentWaypointIndex]);

        // Reset the rotation on the x and z axes to keep the parent object upright
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        // Reset the rotation of the child object to keep the sprite upright
        spriteObject.rotation = Quaternion.Euler(0f, 0f, 0f); ;

        // Check if reached the current waypoint
        if (Vector3.Distance(transform.position, currentPath.vectorPath[currentWaypointIndex]) < waypointReachedThreshold)
        {
            currentWaypointIndex++;
        }
    }





    // Example usage of MoveAlongPath in Update or FixedUpdate
    private void Update()
    {
        MoveAlongPath();
    }
}
