using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject letra;
    public GameObject centro;


    private string palavraOculta = ""; // palavra oculta a ser descoberta
    private int tamanhoPalavraOculta; // tamanho da palavra oculta
    private string[] palavrasOcultas = new string [] {"carro","elefante","futebol"}; // array de palavras ocultas
    char[] letrasOcultas; // letras da palavra oculta
    bool[] letrasDescobertas; // indicador de quais letras foram descobertas

    // Start is called before the first frame update
    void Start()
    {
        centro =  GameObject.Find("centroDaTela");
        InitGame();
        InitLetras();
       
    }

    // Update is called once per frame
    void Update()
    {
        checkTeclado();
    }

    // inicia as letras
    void InitLetras(){
        int numLetras = tamanhoPalavraOculta;
        for(int i =0; i<numLetras; i++){
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i-numLetras/2.0f)*80), centro.transform.position.y, centro.transform.position.z);
            GameObject l =(GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name ="letra" + (i+1);
            l.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }
    void InitGame(){
        int numeroAleatorio = Random.Range(0,palavrasOcultas.Length);
        palavraOculta =palavrasOcultas[numeroAleatorio]; // palavra a ser descoberta
        tamanhoPalavraOculta = palavraOculta.Length; // determina-se o numero de letras da palavra
        palavraOculta = palavraOculta.ToUpper(); // deixar a palavra maiuscula
        letrasOcultas = new char [tamanhoPalavraOculta]; // instancia-se o array char com a quantidade de letras da palavra
        letrasDescobertas = new bool [tamanhoPalavraOculta];// instancia-se o array bool com a quantidade de letras da palavra
        letrasOcultas = palavraOculta.ToCharArray(); // copia a palavra letra por letra no array
    }

    void checkTeclado(){
        if (Input.anyKeyDown)
        {
            char letraTeclada =  Input.inputString.ToCharArray()[0];
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);
            if(letraTecladaComoInt >= 97 &&letraTecladaComoInt<=122){
                for (int i = 0; i < tamanhoPalavraOculta; i++)
                {
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada);
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra"+ (i+1)).GetComponent<Text>().text = letraTeclada.ToString();
                        }
                    }
                }
            }
        }
    }
}
