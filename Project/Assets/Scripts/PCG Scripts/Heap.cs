using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/* inspired by sebastian lagues tutorial: https://www.youtube.com/watch?v=3Dw5d7PlcTM */
public class Heap<T> where T: IHeapItem<T> {

    // -- represent the minimum-heap as a array of generic type <T>
    private T[] heap_array;
    private int count;

    public Heap(int max_size){
        heap_array = new T[max_size];
        count = 0;
    }

    public int Count(){ return count;}
    public bool has(T item){ return Equals(heap_array[item.HeapIndex], item);}

    // -- push a item on the heap
    public void push(T item){
        item.HeapIndex = count;
        heap_array[count] = item;
        fixBottomUp(item);
        count++;
    }

    // -- remove the root of the heap.
    public T pop(){
        T top = heap_array[0];
        count--;
        heap_array[0] = heap_array[count];
        heap_array[0].HeapIndex = 0;
        fixTopDown(heap_array[0]);
        return top;
    }

    // -- fixes the heap given a item
    public void updateItem(T item){ 
        fixBottomUp(item); 
        //fixTopDown(item);
    }

    // -- using the parent-child index property of a array heap
    // -- to swap the current item from bottom to top
    private void fixTopDown(T item){
        while(true){
            // left child of item stored at (2* item_idx) + 1
            // right child of item stored at (2* item_idx) + 2
            int idx = item.HeapIndex; // -- 0
            int left_child_idx = 2*idx + 1;
            int right_child_idx = 2*idx + 2;
            
            if(left_child_idx < count) {
                idx = left_child_idx;
                if(left_child_idx < count){
                    if(heap_array[left_child_idx].CompareTo(heap_array[right_child_idx]) < 0){
                        idx = right_child_idx;
                    }
                }
                if(item.CompareTo(heap_array[idx]) < 0){
                    swap(item, heap_array[idx]);
                }
                else{return;}
            }
            else{return;}
        }
    }

    // -- using the parent child index property of a array heap
    // -- to swap the current item from bottom to top
    private void fixBottomUp(T item){
        // -- node i's parent is at index Ceil(i/2) - 1
        int idx = item.HeapIndex;
        int parent_idx = (item.HeapIndex - 1)/2;

        while(true){
            T parent = heap_array[parent_idx];
            if(item.CompareTo(parent) > 0){swap(item, parent);}
            else{break;}
            parent_idx = (item.HeapIndex - 1)/2;
        }
    }

    // -- method that swaps two Objects in the Heap
    private void swap(T A, T B){
        heap_array[A.HeapIndex] = B;
        heap_array[B.HeapIndex] = A;
        // -- swap idx's
        int idx_A = A.HeapIndex;
        A.HeapIndex = B.HeapIndex;
        B.HeapIndex = idx_A;
    }

}

// -- Define a comparable interface so the user has to implement
// -- ComparTo and Set, and get Heap index
public interface IHeapItem<T> : IComparable<T>{
    int HeapIndex {
        get;
        set;
    } 
}
