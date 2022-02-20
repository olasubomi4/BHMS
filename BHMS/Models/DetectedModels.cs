using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Windows;

namespace BHMS.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Duration
    {
        public string time { get; set; }
        public double seconds { get; set; }
    }

    public class Appearance
    {
        public double confidence { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int startSeconds { get; set; }
        public int endSeconds { get; set; }
    }

    public class Label
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Appearance> appearances { get; set; }
        public string referenceId { get; set; }
        public string language { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class SpeakerTalkToListenRatio
    {
    }

    public class SpeakerLongestMonolog
    {
    }

    public class SpeakerNumberOfFragments
    {
    }

    public class SpeakerWordCount
    {
    }

    public class Statistics
    {
        public int correspondenceCount { get; set; }
        public SpeakerTalkToListenRatio speakerTalkToListenRatio { get; set; }
        public SpeakerLongestMonolog speakerLongestMonolog { get; set; }
        public SpeakerNumberOfFragments speakerNumberOfFragments { get; set; }
        public SpeakerWordCount speakerWordCount { get; set; }
    }

    public class SummarizedInsights
    {
        public string name { get; set; }
        public string id { get; set; }
        public string privacyMode { get; set; }
        public Duration duration { get; set; }
        public string thumbnailVideoId { get; set; }
        public string thumbnailId { get; set; }
        public List<object> faces { get; set; }
        public List<object> keywords { get; set; }
        public List<object> sentiments { get; set; }
        public List<object> emotions { get; set; }
        public List<object> audioEffects { get; set; }
        public List<Label> labels { get; set; }
        public List<object> framePatterns { get; set; }
        public List<object> brands { get; set; }
        public List<object> namedLocations { get; set; }
        public List<object> namedPeople { get; set; }
        public Statistics statistics { get; set; }
        public List<object> topics { get; set; }
    }

    public class Instance
    {
        public string adjustedStart { get; set; }
        public string adjustedEnd { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public double confidence { get; set; }
        public string thumbnailId { get; set; }
    }

    public class Ocr
    {
        public int id { get; set; }
        public string text { get; set; }
        public double confidence { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int angle { get; set; }
        public string language { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Scene
    {
        public int id { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class KeyFrame
    {
        public int id { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Shot
    {
        public int id { get; set; }
        public List<string> tags { get; set; }
        public List<KeyFrame> keyFrames { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Block
    {
        public int id { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class Speaker
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Instance> instances { get; set; }
    }

    public class TextualContentModeration
    {
        public int id { get; set; }
        public int bannedWordsCount { get; set; }
        public int bannedWordsRatio { get; set; }
        public List<object> instances { get; set; }
    }

    public class Insights
    {
        public string version { get; set; }
        public string duration { get; set; }
        public string sourceLanguage { get; set; }
        public List<string> sourceLanguages { get; set; }
        public string language { get; set; }
        public List<string> languages { get; set; }
        public List<Ocr> ocr { get; set; }
        public List<Label> labels { get; set; }
        public List<Scene> scenes { get; set; }
        public List<Shot> shots { get; set; }
        public List<Block> blocks { get; set; }
        public List<Speaker> speakers { get; set; }
        public TextualContentModeration textualContentModeration { get; set; }
        public Statistics statistics { get; set; }
    }

    public class Video
    {
        public string accountId { get; set; }
        public string id { get; set; }
        public string state { get; set; }
        public string moderationState { get; set; }
        public string reviewState { get; set; }
        public string privacyMode { get; set; }
        public string processingProgress { get; set; }
        public string failureCode { get; set; }
        public string failureMessage { get; set; }
        public object externalId { get; set; }
        public object externalUrl { get; set; }
        public object metadata { get; set; }
        public Insights insights { get; set; }
        public string thumbnailId { get; set; }
        public bool detectSourceLanguage { get; set; }
        public string languageAutoDetectMode { get; set; }
        public string sourceLanguage { get; set; }
        public List<string> sourceLanguages { get; set; }
        public string language { get; set; }
        public List<string> languages { get; set; }
        public string indexingPreset { get; set; }
        public string linguisticModelId { get; set; }
        public string personModelId { get; set; }
        public bool isAdult { get; set; }
        public string publishedUrl { get; set; }
        public object publishedProxyUrl { get; set; }
        public string viewToken { get; set; }
    }

    public class Range
    {
        public string start { get; set; }
        public string end { get; set; }
    }

    public class VideosRanx
    {
        public string videoId { get; set; }
        public Range range { get; set; }
    }

    public class Root
    {
        public string partition { get; set; }
        public string description { get; set; }
        public string privacyMode { get; set; }
        public string state { get; set; }
        public string accountId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string userName { get; set; }
        public DateTime created { get; set; }
        public bool isOwned { get; set; }
        public bool isEditable { get; set; }
        public bool isBase { get; set; }
        public int durationInSeconds { get; set; }
        public SummarizedInsights summarizedInsights { get; set; }
        public List<Video> videos { get; set; }
        public List<VideosRanx> videosRanges { get; set; }
    }



}


