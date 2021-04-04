using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health = 100;
    public float Health { get { return health; } set { health = value; if (health <= 0 && gameObject) { Destroy(gameObject); } } }
    public Animator Animator { get; private set; }
    [SerializeField] TextMeshProUGUI hpBar;
    [SerializeField] ButtonSlot  btmele, ladder;
    [SerializeField] AudioClip fallingDownStairs;
    bool isFalling = false;
    Room[,] rooms = new Room[,]
    {
        {new Room(RoomType.Normal, new Vector2(-3,2.3f)),new Room(RoomType.Normal, new Vector2(-3,0)) , new Room(RoomType.Normal, new Vector2(-3,-4.08f)) },
        {new Room(RoomType.Stairs, new Vector2(0,2.3f)),new Room(RoomType.Stairs, new Vector2(0,0)) ,new Room(RoomType.Normal, new Vector2(0,-4.08f))  },
         {new Room(RoomType.Normal, new Vector2(2.5f,2.3f)),new Room(RoomType.Stairs, new Vector2(3.45f,0)) , new Room(RoomType.Stairs, new Vector2(3.45f,-4.08f)) },
    };
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] int2 room;
    bool moveOn = true;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.text = Health.ToString();
        if (moveOn)
        {
            moveOn = false;
            StartCoroutine(MasterAI());
        }
        
    }
    IEnumerator MasterAI()
    {
        var destination = RandomDestination(room);
        if(room.x != destination.x || room.y != destination.y)
        {
            Animator.SetBool("IsWalking", true);
            
            while (true)
            {
                if((Vector2)transform.position == rooms[destination.x, destination.y].position) { break; }
                Vector2 dest = rooms[destination.x, destination.y].position;
                var newPos = Vector2.MoveTowards(transform.position, dest, 0.1f);
                
                if (transform.position.x - newPos.x != math.abs(transform.position.x - newPos.x))
                    { Animator.gameObject.transform.localScale = 
                            new Vector3(math.abs(Animator.gameObject.transform.localScale.x), Animator.gameObject.transform.localScale.y, Animator.gameObject.transform.localScale.z); }
                else
                    {
                    Animator.gameObject.transform.localScale = new Vector3(-math.abs(Animator.gameObject.transform.localScale.x),
                            Animator.gameObject.transform.localScale.y, Animator.gameObject.transform.localScale.z); }
                
                transform.position = newPos;
                yield return new WaitForSeconds(0.05f);
            } 
            Animator.SetBool("IsWalking", false);
            room = destination;
           
        }
        if (isFalling)
        {
            Health -= 5;
            isFalling = false;
            AudioSource.PlayClipAtPoint(fallingDownStairs, transform.position,2);
        }
        yield return new WaitForSeconds(3);
        moveOn = true;
    }
    int2 RandomDestination(int2 location)
    {
        if(rooms[location.x,location.y].roomType == RoomType.Normal)
        {
            return new int2(UnityEngine.Random.Range(0, 3), location.y);
        }
        else
        {
            if (UnityEngine.Random.Range(0, 3) == 1)
            {
                if (location.x == 1 && location.y == 0) { return new int2(1, 1); }
                if (location.x == 1 && location.y == 1 && btmele.IsEngaged) { return new int2(1, 0); }
                if (location.x == 2 && location.y == 1) { if (!ladder.IsEngaged) { isFalling = true; } return new int2(2, 2); }
                if (location.x == 2 && location.y == 2) { return new int2(2, 1); }
            }
            else
            {
                return new int2(UnityEngine.Random.Range(0, 3), location.y);
            }
        }
        return new int2(UnityEngine.Random.Range(0, 3), location.y);
    }
    enum RoomType
    {
        Stairs,
        Normal
    }
    struct Room
    {
        public RoomType roomType;
        public Vector2 position;
        public Room(RoomType rt, Vector2 pos) => (roomType, position) = (rt, pos);
    }
}

