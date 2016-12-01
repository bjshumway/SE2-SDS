using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Save {
    public static int char1level;
    public static int char2level;
    public static int char3level;

    public static int[] char1skills;
    public static int[] char2skills;
    public static int[] char3skills;

    public static int char1health;
    public static int char2health;
    public static int char3health;

    public static int char1stam;
    public static int char2stam;
    public static int char3stam;

    [XmlIgnore]
    public static string[] char1abilities;
    public static string[] char2abilities;
    public static string[] char3abilities;

    public static int char1skillpoints;
    public static int char2skillpoints;
    public static int char3skillpoints;

    public static int char1abilitypoints;
    public static int char2abilitypoints;
    public static int char3abilitypoints;

    
}
