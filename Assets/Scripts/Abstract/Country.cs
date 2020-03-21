using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public abstract class Country : MonoBehaviour
{

    
    public string countryName;                                      //Name of the country

    public bool democratic;                                         //Whether the country is democratic or not
    public static Dictionary<string,LawNode> laws;                  //List of law trees that can be enforced
    
    public int startingBudget;                                      //The starting budget for a country
    public int currentBudget;                                       //The current budget for a country

    public float susceptible => states.Sum(s => s.susceptible);
    public float exposed => states.Sum(s => s.exposed);
    public float infected => states.Sum(s => s.infected);
    public float hospitalized => states.Sum(s => s.hospitalized);
    public float critical => states.Sum(s => s.critical);
    public float dead => states.Sum(s => s.dead);
    public float recovered => states.Sum(s => s.recovered);
    public float population => states.Sum(s => s.population);         //Total population of the country

    public List<State> states;                                      //List of states in the country

    public List<Country> neighbours;                                //List of neighbouring countries

    public string json;

    //Enforce a specific law (Depending on implementation democratic or totalitarian)
    public abstract void EnforceLaw(Law law);

    //Import Laws from json files
    public virtual void ReadLaws()
    {
        Debug.LogWarning("Please implement proper directory when reading JSON Files!!!");
        string dir = ".\\Assets\\Laws";

        laws = new Dictionary<string, LawNode>();
        
        string[] paths = Directory.GetFiles(dir);
        //Debug.Log(paths);
        foreach(string path in paths)
        {
            //If JSON File
            if (path.Split(".".ToCharArray())[path.Split(".".ToCharArray()).Length - 1] == "json")
            {
                //File Name
                //Debug.Log(path.Split("\\".ToCharArray())[path.Split("\\".ToCharArray()).Length - 1]);

                StreamReader file = File.OpenText(path);
                string text = file.ReadToEnd();                       //Get file contents as string
                file.Close();


                //Clean up json
                text = text.Replace("\r\n", "");
                text = text.Replace("            ", "");
                text = text.Replace("    ", "");
                //Debug.Log(text);


                string name = path.Split("\\".ToCharArray())[path.Split("\\".ToCharArray()).Length - 1].Split(".".ToCharArray())[0];         //Get file name <name>.json by splitting at .
                laws.Add(name, JsonUtility.FromJson<LawNode>(text));    //Add law tree into dictionary
            }

        }
        Debug.Log(laws["Hygiene"].subNode[0].law.name);
        /// DEBUG ZONE  ///
        //Debug.Log(laws);
        //LawNode test = new LawNode(new Law("Washing Hands", 10, 0.1f),true).AddTree(new LawNode(new Law("Washing Hands", 10, 0.1f))).subNode[0].AddTree(new LawNode(new Law("Verteilen von Taschentüchern",1,0.01f))).AddTree(new LawNode(new Law("Verteilen von Disinfektionmittel", 1, 0.01f))).prev;
        //List<LawNode> test = new List<LawNode>() { new LawNode(new Law("Washing Hands", 10, 0.1f)), new LawNode(new Law("Verteilen von Taschentüchern", 10, 0.1f)).AddTree(new LawNode(new Law("Verteilen von Disinfektionmittel", 1, 0.01f))).AddTree(new LawNode(new Law("Oiiiii", 1, 0.01f))) };

        /*string temp = JsonUtility.ToJson(test);
        Debug.Log(temp);
        LawNode tmp = JsonUtility.FromJson<LawNode>(temp);
        Debug.Log(tmp.subNode[0].law.name);*/
        //Debug.Log(test.subNode[0].subNode[0].law.name);
        //LawNode temp = JsonUtility.FromJson<LawNode>();
    }

}
