using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent NoCollidersRemain;
    Collider2D col;
    public List<Collider2D> detectedCollieders = new List<Collider2D>();
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollieders.Add(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollieders.Remove(collision);

        if(detectedCollieders.Count <= 0 )
        {
            NoCollidersRemain.Invoke();
        }
    }
}
