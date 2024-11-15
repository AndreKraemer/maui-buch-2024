﻿using System.Text;
using System.Text.Json;
using MonkeyCache.FileStore;
using WebserviceSample.Models;

namespace WebserviceSample.Services;

public class SpeakerService : ISpeakerService
{
  private static readonly HttpClient _client = new HttpClient
  {
    BaseAddress = new Uri(
          "https://my-json-server.typicode.com/AndreKraemer/ConferenceAppDemoData/")
  };

  private static JsonSerializerOptions _serializerOptions
      = new()
      {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      };

  public async Task<bool> CreateAsync(Speaker speaker)
  {
    var json = JsonSerializer.Serialize(speaker, _serializerOptions);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    var result = await _client.PostAsync("speakers", content);
    return result.IsSuccessStatusCode;
  }

  public async Task<bool> UpdateAsync(Speaker speaker)
  {
    var json = JsonSerializer.Serialize(speaker, _serializerOptions);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    var result = await _client.PutAsync($"speakers/{speaker.Id}", content);
    return result.IsSuccessStatusCode;
  }

  public async Task<bool> DeleteAsync(int id)
  {
    var result = await _client.DeleteAsync($"speakers/{id}");
    return result.IsSuccessStatusCode;
  }

  public async Task<Speaker> GetAsync(int id)
  {
    var json = await _client.GetStringAsync($"speakers/{id}");
    var speaker = JsonSerializer.Deserialize<Speaker>(json, _serializerOptions);
    return speaker;
  }

  public async Task<IEnumerable<Speaker>> GetAsync(bool forceRefresh = false)
  {
    // Wenn keine Internetverbindung besteht gecachte Daten zurückgeben,
    // auch wenn diese bereits abgelaufen sind.
    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
    {
      return Barrel.Current.Get<IEnumerable<Speaker>>("speakers") ?? [];
    }

    // Falls Internetverbindung besteht und es nicht abgelaufene
    // gecachte Daten gibt: Diese zurückgeben
    if (!Barrel.Current.IsExpired("speaker") && !forceRefresh)
    {
      return Barrel.Current.Get<IEnumerable<Speaker>>("speakers");
    }

    // Daten vom Webservice abrufen
    var json = await _client.GetStringAsync("speakers");
    var speakers = JsonSerializer.Deserialize<List<Speaker>>(json, _serializerOptions);

    // Daten mit einer Lebensdauer von 5 Minuten im Cache ablegen
    Barrel.Current.Add("speakers", speakers, TimeSpan.FromMinutes(5));
    return speakers;
  }
}