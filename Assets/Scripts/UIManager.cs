using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class UIManager : MonoBehaviour
{
    
    public GameObject rowPrefab;    
    public Transform contentPanel;
    public InputField searchBar;
    public Toggle studentToggle;
    public Toggle teacherToggle;
    public Toggle sortByNameToggle;
    public Toggle sortByAgeToggle;
    public Toggle sortByScoreToggle;

    private List<Person> currentList = new List<Person>();
    private bool showingStudents = true;

    void Start()
    {
        
        studentToggle.isOn = true;
        LoadStudents();
        studentToggle.onValueChanged.AddListener(OnStudentToggle);
        teacherToggle.onValueChanged.AddListener(OnTeacherToggle);

        sortByNameToggle.onValueChanged.AddListener(OnSortByNameToggle);
        sortByAgeToggle.onValueChanged.AddListener(OnSortByAgeToggle);
        sortByScoreToggle.onValueChanged.AddListener(OnSortByScoreToggle);

        searchBar.onValueChanged.AddListener(delegate { OnSearchChanged(); });
    }

 
    public void DisplayList(List<Person> people)
    {
        
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

       
        if (people.Count == 0)
        {
            GameObject row = Instantiate(rowPrefab, contentPanel);
            row.transform.Find("NameText").GetComponent<Text>().text = "";
            row.transform.Find("AgeText").GetComponent<Text>().text = "";
            row.transform.Find("ScoreText").GetComponent<Text>().text = "";
            return;
        }

        
        int index = 0;
        foreach (var person in people)
        {
            GameObject row = Instantiate(rowPrefab, contentPanel);

            
            var bg = row.GetComponent<Image>();
            if (bg != null)
                bg.color = (index % 2 == 0) ? new Color(0.9f, 0.9f, 0.9f) : Color.cyan;

            row.transform.Find("NameText").GetComponent<Text>().text = person.Name;
            row.transform.Find("AgeText").GetComponent<Text>().text = person.Age.ToString();

            float score = (person is Student s) ? s.Score : ((Teacher)person).Score;
            row.transform.Find("ScoreText").GetComponent<Text>().text = score.ToString();

            index++;
        }
    }

  
    void OnStudentToggle(bool isOn) { if (isOn) LoadStudents(); }
    void OnTeacherToggle(bool isOn) { if (isOn) LoadTeachers(); }

    public void LoadStudents()
    {
        showingStudents = true;
        currentList = DataManager.Students.Cast<Person>().ToList();
        DisplayList(currentList);
    }

    public void LoadTeachers()
    {
        showingStudents = false;
        currentList = DataManager.Teachers.Cast<Person>().ToList();
        DisplayList(currentList);
    }

   
    void OnSortByNameToggle(bool isOn) { if (isOn) DisplayList(currentList.OrderBy(p => p.Name).ToList()); }
    void OnSortByAgeToggle(bool isOn) { if (isOn) DisplayList(currentList.OrderByDescending(p => p.Age).ToList()); }
    void OnSortByScoreToggle(bool isOn)
    {
        if (isOn)
        {
            DisplayList(currentList.OrderByDescending(p =>
                (p is Student s) ? s.Score : ((Teacher)p).Score).ToList());
        }
    }

 
    public void OnSearchChanged()
    {
        string keyword = searchBar.text.ToLower();
        var filtered = currentList.Where(p => p.Name.ToLower().Contains(keyword)).ToList();
        DisplayList(filtered);
    }
}
