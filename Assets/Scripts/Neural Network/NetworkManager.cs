using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

    public IntIntDoubleDictionary weightDict;
    public IntIntDoubleDictionary biasDict;

    protected internal NeuralNetwork network;

    private Layer[] layers;

    private void Awake() {
        network = new NeuralNetwork(2, 3, 2);
        layers = network.GetLayers();

        for (int i = 0; i < layers.Length; i++) {
            double[] weights = layers[i].GetWeights();
            weightDict.Add(i, new IntDoubleDictionary());
            for (int j = 0; j < weights.Length; j++) {
                weightDict[i].Add(j, weights[j]);
            }
            
            double[] biases = layers[i].GetBiases();
            biasDict.Add(i, new IntDoubleDictionary());
            for (int j = 0; j < biases.Length; j++) {
                biasDict[i].Add(j, biases[j]);
            }
        }
    }
}
