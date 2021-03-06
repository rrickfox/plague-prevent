﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using TMPro;

public abstract class Country : MonoBehaviour
{
    public IDisease disease;

    public string countryName;                                      //Name of the country
    public Graph graph;

    public bool democratic;                                         //Whether the country is democratic or not
    public static Dictionary<string,LawNode> laws;                  //List of law trees that can be enforced
    public List<Law> enforcedLaws = new List<Law>();                //List of laws than are currently being enforced              
    
    public int startingBudget;                                      //The starting budget for a country
    public int currentBudget;                                       //The current budget for a country
    public float satisfaction=1f;                                   //How happy the general population is


    public float susceptible => states.Sum(s => s.susceptible);
    public float exposed => states.Sum(s => s.exposed);
    public float infected => states.Sum(s => s.infected);
    public float hospitalized => states.Sum(s => s.hospitalized);
    public float critical => states.Sum(s => s.critical);
    public float dead => states.Sum(s => s.dead);
    public float recovered => states.Sum(s => s.recovered);
    public float population => states.Sum(s => s.population);         //Total population of the country
    
    public List<float> susceptibleHistory = new List<float>();
    public List<float> infectedHistory = new List<float>(){0};
    public List<float> recoveredHistory = new List<float>(){0};
    public List<float> deadHistory = new List<float>(){0};


    public float ticks = 0f;
    public float seconds = 0f;
    public System.Random random = new System.Random();
    public float chanceOfInfectedToTravel = 0.001f;
    public float chanceOfTravellerToInfect = 0.5f;


    public bool enforcingLaw = false;                               //Whether the country is enforcing a law
    public float lawEnforcementProgress = 0f;                       //Progress of the law enforcement


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
        
        string[] paths = Directory.GetFiles(dir);   //Get Files in path
        foreach (string path in paths)
        {
            //If JSON File
            if (path.Split(".".ToCharArray())[path.Split(".".ToCharArray()).Length - 1] == "json")
            {
                StreamReader file = File.OpenText(path);
                string text = file.ReadToEnd();                       //Get file contents as string
                file.Close();


                //Clean up json
                text = text.Replace("\r\n", "");
                text = text.Replace("            ", "");
                text = text.Replace("    ", "");


                //File Name
                string name = path.Split("\\".ToCharArray())[path.Split("\\".ToCharArray()).Length - 1].Split(".".ToCharArray())[0];         //Get file name <name>.json by splitting at .
                LawNode node = JsonUtility.FromJson<LawNode>(text);                                                                         //Convert json to LawNode object   
                node.UpdatePrev();                                                                                                          //Adds missing previous attribute to nodes
                node.UpdateChildIndex();
                laws.Add(name, node);    //Add law tree into dictionary
            }

        }
        //Debug.Log(laws["Hygiene"].subNode[0].law.name);
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


    public TextMeshProUGUI daysText;


    public virtual void UpdateTick(IDisease disease)
    {
        ticks++;
        if (ticks == 50)
        {
            ticks = 0;
            seconds++;
            foreach (var state in states)
                state.CalculateInfectionRates(disease);
            
            foreach(var state in states)
            {
                if(random.NextDouble() <= state.infected * chanceOfInfectedToTravel * chanceOfTravellerToInfect)
                {
                    state.neighbouring[random.Next(state.neighbouring.Count)].Infect();
                }
            }
            daysText.text = string.Format("Tag: {0}", Mathf.Floor(seconds / (Constants.timeScale * 2f)));


            currentBudget += (int)(satisfaction * (population / 1000000f));
            satisfaction -= 1f;
            satisfaction = Mathf.Clamp(satisfaction, 0f, Mathf.Infinity);

            //Debug.Log(string.Format("Current Budget: {0}", (Constants.timeScale * 2f)));

            susceptibleHistory.Add(susceptible);
            infectedHistory.Add(exposed + infected + hospitalized + critical);
            recoveredHistory.Add(recovered);
            deadHistory.Add(dead);

            graph.UpdateLines();
        }
    }
}
