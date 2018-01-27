using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerEntity Entity; 
}

public enum PlayerEntity
{
    Blue, Red, Yellow, Green, Neutral
}