type Activity = {
  id: string
  title: string
  date: Date
  description: string
  category: string
  isCancelled: boolean
  city: string
  venue: string
  latitude: number
  longitude: number
}

type User = {
  id: string,
  email: string,
  displayName: string,
  iamgeUrl?: string
}

type LocationIQSuggestion = {
  place_id: string
  licence: string
  osm_type: string
  osm_id: string
  lat: string
  lon: string
  display_name: string
  address: LocationIQAddress
  boundingbox: string[]
}

type LocationIQAddress = {
  road: string
  suburb: string
  town?: string
  village?: string
  city?: string
  county: string
  state: string
  postcode: string
  country: string
  country_code: string
}
