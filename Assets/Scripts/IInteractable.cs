using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sučelje za interaktabilne vrste objekata (zamke, modifikacije terena, itd.)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IInteractable<T>{
	bool Triggered { get; set; } // kako ne bi doslo do nepotrebnog drugog pokretanja efekta, ovaj okidac to onemogucuje
	T Effect(); // metoda koja definira određeni efekt, ovisno o tipu objekta
}
