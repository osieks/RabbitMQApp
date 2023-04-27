# RabbitMQApp
Producer and 3 Consumers for an Exchange using RabbitMQ

EUVIC
System kolejkowania zgłoszeń na bazie RabbitMQ -

Opis zadania:

Przygotuj system dystrybucji zgłoszeń oparty o kilka lekkich aplikacji i kolejkę RabbitMQ. Wymagania szczegółowe znajdziesz poniżej: 

      Kolejka oparta o RabbitMQ
    Exchange obsługujący trzy typy zdarzeń:
    - prośbę o pomiar energii elektrycznej
    - prośbę o fakturę
    - zgłoszenie o awarii

     Producent zdarzeń (lekka aplikacja konsolowa
    - Produkuje losowo jedno z trzech typów zdarzeń z zadaną częstotliwością, np. co sekundę
    - Po wyprodukowaniu zdarzenia aplikacja wyświetla na konsoli informację: „Wygenerowano zgłoszenie typu {typZgłoszenia} o identyfikatorze {GUID}”

     Konsumenci zdarzeń
    - Trzy instancje konsolowych aplikacji, konsumujące wybrany typ zdarzenia
    - Aplikacja po przyjęciu zgłoszenia wyświetla na konsoli informację: „Uruchomiono obsługę zgłoszenia”
    - Czeka zadany czas, w zależności od rodzaju zgłoszenia:
    a. Prośba o pomiar energii elektrycznej (1 sekunda)
    b. Prośba o fakturę (2 sekundy)
    c. Zgłoszenie o awarii (3 sekundy) 

    - Po odczekaniu zadanego czasu wyświetla na konsolę informację: „Obsłużono zgłoszenie typu {typZgłoszenia} o identyfikatorze: {GUID}” 
