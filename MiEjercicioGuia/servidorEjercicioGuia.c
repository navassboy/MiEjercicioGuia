#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <ctype.h>  // para toupper y tolower
#include <pthread.h> //para mutex

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

void *Atender_cliente(void *socket_desc){
	
		int sock_conn = ](int*)socket_desc;
		free(socket_desc);
		
		char peticion[512];
		char respuesta[512];


		ret = read(sock_conn, peticion, sizeof(peticion));
        printf("Recibida una peticion\n");

        peticion[ret] = '\0';  // Aￃﾱade el fin de cadena

        printf("La peticion es: %s\n", peticion);
		pthread_mutex_lock(&mutex); //No me interrumpas
		
        char *p = strtok(peticion, "/");
        int codigo = atoi(p);  // Primer valor: el cￃﾳdigo
        p = strtok(NULL, "/");
        char nombre[20];
        strcpy(nombre, p);  // Segundo valor: el nombre

        printf("Codigo: %d, Nombre: %s\n", codigo, nombre);

        if (codigo == 1) {
            // Devuelve la longitud del nombre
            sprintf(respuesta, "%d", strlen(nombre));
        } else if (codigo == 2) {
            // Devuelve si el nombre es bonito
            if ((nombre[0] == 'I') || (nombre[0] == 'A'))
                strcpy(respuesta, "SI");
            else
                strcpy(respuesta, "NO");
        } else if (codigo == 3) {
            // Devuelve si es alto o bajo
            p = strtok(NULL, "/");
            float altura = atof(p);
            if (altura > 1.70)
                sprintf(respuesta, "%s: eres alto", nombre);
            else
                sprintf(respuesta, "%s: eres bajo", nombre);
        } else if (codigo == 4) {
            // Verificar si el nombre es palￃﾭndromo
            int len = strlen(nombre);
            int es_palindromo = 1;  // Asumimos que es palￃﾭndromo
            for (int i = 0; i < len / 2; i++) {
                if (tolower(nombre[i]) != tolower(nombre[len - 1 - i])) {
                    es_palindromo = 0;  // No es palￃﾭndromo
                    break;
                }
            }
            if (es_palindromo)
                sprintf(respuesta, "%s es un paliﾭndromo", nombre);
            else
                sprintf(respuesta, "%s no es un paliﾭndromo", nombre);
        } else if (codigo == 5) {
            // Convertir el nombre a mayￃﾺsculas
            for (int i = 0; nombre[i]; i++) {
                nombre[i] = toupper(nombre[i]);
            }
            sprintf(respuesta, "Tu nombre en mayusculas es: %s", nombre);
        } else {
            sprintf(respuesta, "Codigo de servicio no valido");
        }

        // Enviar la respuesta
        write(sock_conn, respuesta, strlen(respuesta));
		
		pthread_mutex_unlock(&mutex); //Ya puedes

        // Cerrar la conexiￃﾳn con el cliente
        close(sock_conn);
    }

int main(int argc, char *argv[]) {
    int sock_conn, sock_listen, ret;
    struct sockaddr_in serv_adr;
  

    // Inicializaciones
    if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
        printf("Error creando socket");

    memset(&serv_adr, 0, sizeof(serv_adr));  // Inicializa a cero serv_addr
    serv_adr.sin_family = AF_INET;
    serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);  // Asocia el socket a cualquier IP de la mￃﾡquina
    serv_adr.sin_port = htons(9060);  // Escucharemos en el puerto 9070

    if (bind(sock_listen, (struct sockaddr *)&serv_adr, sizeof(serv_adr)) < 0)
        printf("Error al bind");

    if (listen(sock_listen, 4) < 0)
        printf("Error en el listen");

    int i;
    while(1) {
        printf("Escuchando\n");

        sock_conn = accept(sock_listen, NULL, NULL);
        printf("He recibido conexion\n");
		
		pthread_t cliente_thread;
		int *new_sock = malloc(1);
		*new_sock = sock_conn;
		pthread_create(&client_thread, NULL, handle_client, (void*) new_sock);
	}
}


