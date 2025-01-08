FROM nginx:alpine
RUN mkdir -p /etc/nginx/ssl

COPY ./certificate/certificate.crt /etc/nginx/ssl/certificate.crt
COPY ./certificate/certificate.key /etc/nginx/ssl/certificate.key

COPY ./nginx.conf /etc/nginx/nginx.conf
EXPOSE 443
CMD ["nginx", "-g", "daemon off;"]