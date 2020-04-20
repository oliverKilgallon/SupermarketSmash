using UnityEngine;

/// <summary>
/// Spawn a random object at regular intervals chosen from a passed in model list with a passed in object spacing offset
/// </summary>
public class ItemPopulator : MonoBehaviour
{
    [Tooltip("List of models for the spawner to choose from.")]
    public GameObject[] m_Model_List;
    [Tooltip("The amount of times the spawner will sample from the model list.")]
    public int m_Number_To_Spawn;
    [Tooltip("The amount of units to space each object apart by.")]
    public float m_Object_Spacing;
    [Tooltip("The y-coord of each object is offset by this amount. Use this if the spawner is inside the shelf.")]
    public float m_Shelf_Offset;
    [Tooltip("High end of the random angle added to the object rotation.")]
    public float m_Max_Angle_Offset;
    [Tooltip("Low end of the random angle added to the object rotation.")]
    public float m_Min_Angle_Offset;
    [Tooltip("Maximum value an object can be scaled by in all directions. Object scale will be between 1 and this value.")]
    public float m_Max_Scale_Offset;

    /// <summary>
    /// Axis to spawn objects along.
    /// </summary>
    public enum axis
    {
        X_AXIS,
        Y_AXIS,
        Z_AXIS
    }
    [Tooltip("The axis that the objects are spawned along. This axis is in world space.")]
    public axis m_Row_Axis;

    /// <summary>
    /// Base angle to face objects at.
    /// </summary>
    public enum objectFacing
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }
    [Tooltip("The base angle that the objects are faced in prior to offset being added.")]
    public objectFacing m_Object_Direction;

    /// <summary>
    /// Spawn random objects from the model list up to the number of objects to spawn, defined by m_Number_To_Spawn.
    /// </summary>
    private void Awake()
    {
        for (int i = GetInitSpawnValue(m_Number_To_Spawn); i <= -GetInitSpawnValue(m_Number_To_Spawn); i++)
        {
            if (m_Model_List.Length.Equals(1)) SpawnObject(RandIntNoNegative(m_Model_List.Length + 1, true), i);
            SpawnObject(RandIntNoNegative(m_Model_List.Length, true), i);
        }
    }

    /// <summary>
    /// Get the intial value to start spawning objects at.
    /// </summary>
    /// <param name="numberToSpawn">The total amount of objects to spawn</param>
    /// <returns>Negative integer value that will be half of the total number of objects to spawn, properly rounded if the total was odd</returns>
    private int GetInitSpawnValue(int numberToSpawn)
    {
        if (numberToSpawn % 2 == 0) return -(numberToSpawn / 2);
        else
        {
            int halvedNum = (numberToSpawn + 1) / 2;
            if (halvedNum % 2 == 0)
            {
                return -(halvedNum - 1);
            }
        }

        return 0;
    }

    /// <summary>
    /// Returns a random integer greater or equal to 0 based on the size of the passed in integer.
    /// </summary>
    /// <param name="maxInt">Maximum range of the random integer selection</param>
    /// <param name="isZeroIncluded">Allow zero to be used for low end of scale, 1 is used if this value is false</param>
    /// <returns>Random integer between 0 and maxInt</returns>
    private int RandIntNoNegative(int maxInt, bool isZeroIncluded)
    {
        return isZeroIncluded ? Random.Range(0, maxInt) : Random.Range(1, maxInt);
    }

    /// <summary>
    /// Return a random float between minFloat[Inclusive] and maxFloat[Inclusive].
    /// </summary>
    /// <param name="minFloat">Low end of the float range</param>
    /// <param name="maxFloat">High end of the float range</param>
    /// <returns>Random float between minFloat and maxFloat</returns>
    private float RandFloat(float minFloat, float maxFloat)
    {
        return Random.Range(minFloat, maxFloat);
    }

    /// <summary>
    /// Return a random float between the negative[Inclusive] and positive[Inclusive] value of the passed in limit. Use this to have a uniform range.
    /// </summary>
    /// <param name="limits">Float to be used for both the negative low end of the limit and the positive high end of the limit</param>
    /// <returns>Random float between negative value of limits and positive value of limits</returns>
    private float RandFloat(float limits)
    {
        return Random.Range(-limits, limits);
    }

    /// <summary>
    /// Spawn a random object from the model list, offset by the object num.
    /// </summary>
    /// <param name="randNumber">Randomly chosen object from model list. If 0, space is left empty</param>
    /// <param name="objNum">Where in the row/Column the object is. Used to offset object position</param>
    private void SpawnObject(int randNumber, int objNum)
    {
        GameObject objectToSpawn;
        float objectScale = RandFloat(1.0f, m_Max_Scale_Offset);

        try
        {
            //If randNumber is 0, nothing is instantiated and instead an empty space is left.
            switch (m_Row_Axis)
            {
                case axis.X_AXIS:
                    if (!randNumber.Equals(0))
                    {
                        objectToSpawn = Instantiate(m_Model_List[randNumber], gameObject.transform.position, GetObjectRotation(m_Model_List[randNumber].transform.rotation.eulerAngles.x, m_Model_List[randNumber].transform.rotation.eulerAngles.z), gameObject.transform);
                        //objectToSpawn.transform.localScale = Vector3.one * objectScale * objectToSpawn.transform.localScale.magnitude;
                        objectToSpawn.transform.position += new Vector3(objNum * m_Object_Spacing, m_Model_List[randNumber].GetComponent<MeshRenderer>().bounds.extents.y + (m_Shelf_Offset * objectScale), 0);
                    }
                    break;

                case axis.Y_AXIS:
                    if (!randNumber.Equals(0))
                    {
                        objectToSpawn = Instantiate(m_Model_List[randNumber], gameObject.transform.position, GetObjectRotation(m_Model_List[randNumber].transform.rotation.eulerAngles.x, m_Model_List[randNumber].transform.rotation.eulerAngles.z), gameObject.transform);
                        //objectToSpawn.transform.localScale = Vector3.one * objectScale;
                        objectToSpawn.transform.position += new Vector3(0, objNum * m_Object_Spacing, 0);
                    }
                    break;

                case axis.Z_AXIS:
                    if (!randNumber.Equals(0))
                    {
                        objectToSpawn = Instantiate(m_Model_List[randNumber], gameObject.transform.position, GetObjectRotation(m_Model_List[randNumber].transform.rotation.eulerAngles.x, m_Model_List[randNumber].transform.rotation.eulerAngles.z), gameObject.transform);
                        //objectToSpawn.transform.localScale = Vector3.one * objectScale;
                        objectToSpawn.transform.position += new Vector3(0, m_Model_List[randNumber].GetComponent<MeshRenderer>().bounds.extents.y + (m_Shelf_Offset * objectScale), objNum * m_Object_Spacing);
                    }
                    break;

                default:
                    if (!randNumber.Equals(0))
                    {
                        objectToSpawn = Instantiate(m_Model_List[randNumber], gameObject.transform.position, GetObjectRotation(m_Model_List[randNumber].transform.rotation.eulerAngles.x, m_Model_List[randNumber].transform.rotation.eulerAngles.z), gameObject.transform);
                        //objectToSpawn.transform.localScale = Vector3.one * RandFloat(1.0f, m_Max_Scale_Offset);
                        objectToSpawn.transform.position += new Vector3(objNum * m_Object_Spacing, m_Model_List[randNumber].GetComponent<MeshRenderer>().bounds.extents.y + (m_Shelf_Offset * objectScale), 0);
                    }
                    break;
            }
        }
        catch (System.Exception ex)
        {
            if (ex is MissingComponentException)
            {
                print("Attempting to get the bounds of an empty space");
                print(ex.StackTrace);
            }
            else if (ex is UnassignedReferenceException)
            {
                print("Attempting to spawn from empty slot in model list");
                print(ex.StackTrace);
            }
        }

    }

    /// <summary>
    /// Gets the euler angle of a unit vector dependant on the objectFacing enum.
    /// </summary>
    /// <returns>A quaternion rotation</returns>
    private Quaternion GetObjectRotation(float objectXRot, float objectZRot)
    {
        Vector3 rotVector = new Vector3(0, 90.0f, 0);
        switch (m_Object_Direction)
        {
            case objectFacing.NORTH:
                return Quaternion.Euler((rotVector * 0) + new Vector3(objectXRot, RandFloat(m_Min_Angle_Offset, m_Max_Angle_Offset), objectZRot));
            case objectFacing.EAST:
                return Quaternion.Euler((rotVector * 1) + new Vector3(objectXRot, RandFloat(m_Min_Angle_Offset, m_Max_Angle_Offset), objectZRot));
            case objectFacing.SOUTH:
                return Quaternion.Euler((rotVector * 2) + new Vector3(objectXRot, RandFloat(m_Min_Angle_Offset, m_Max_Angle_Offset), objectZRot));
            case objectFacing.WEST:
                return Quaternion.Euler((rotVector * 3) + new Vector3(objectXRot, RandFloat(m_Min_Angle_Offset, m_Max_Angle_Offset), objectZRot));
            default:
                return Quaternion.Euler((rotVector * 0) + new Vector3(objectXRot, RandFloat(m_Min_Angle_Offset, m_Max_Angle_Offset), objectZRot));
        }
    }
}
