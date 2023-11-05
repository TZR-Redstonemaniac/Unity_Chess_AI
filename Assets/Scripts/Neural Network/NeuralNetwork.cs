using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeuralNetwork {
        
    private readonly Layer[] layers;

    public NeuralNetwork(params int[] layerSizes){
        layers = new Layer[layerSizes.Length - 1];
        for (int i = 0; i < layers.Length; i++) {
            layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
        }
    }

    private double[] CalculateOutputs (double[] inputs) {
        return layers.Aggregate(inputs, (current, layer) => layer.CalculateOutputs(current));
    }

    public int Classify (params double[] inputs){
        double[] outputs = CalculateOutputs(inputs);

        return IndexOfMaxValue(outputs);
    }

    private int IndexOfMaxValue(double[] array) {
        int maxIndex = 0;  // Initialize the index of the maximum value to 0.
        double maxValue = array[0];  // Initialize the maximum value to the first element.

        for (int i = 1; i < array.Length; i++) {
            if (!(array[i] > maxValue)) continue;
            maxValue = array[i];
            maxIndex = i;
        }

        return maxIndex;
    }

    public Layer[] GetLayers() {
        return layers;
    }
    
}
