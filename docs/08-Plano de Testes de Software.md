# Plano de Testes de Software

# Casos de Teste

| ID do teste | Requisito associado | Objetivo do teste | Passos | Critérios de êxito |
|-------------|---------------------|------------------|--------|---------------------|
| CT-001 | RF-001 | Validar busca por localização (lista e mapa) | 1) Acessar Home 2) Informar CEP válido e raio 3) Clicar 'Buscar' 4) Alternar lista/mapa | Lista de empresas com distância; marcadores no mapa; paginação/ordenação sem erros |
| CT-002 | RF-002 | Validar retorno sem resultados | 1) Buscar por CEP sem cobertura | Mensagem clara de 'Sem empresas na região' e sugestão de ampliar raio |
| CT-003 | RF-003 | Validar perfil detalhado da empresa | 1) Abrir perfil pela busca | Descrição, serviços, contatos e área de atendimento visíveis e consistentes |
| CT-004 | RF-004 | Validar exibição de avaliações | 1) Abrir aba 'Avaliações' | Média calculada, comentários paginados; filtros funcionam |
| CT-005 | RF-005 | Validar resposta da empresa à solicitação | 1) Logar como empresa 2) Abrir solicitações 3) Enviar proposta | Proposta registrada; cliente notificado; status atualizado |
| CT-006 | RF-006 | Validar comparação de orçamentos | 1) Selecionar 2+ propostas 2) Abrir 'Comparar' | Tabela com preço/prazo/garantia; destaque melhor opção |
| CT-007 | RF-007 | Validar agendamento de visita | 1) Escolher data/horário disponível 2) Confirmar | Visita criada; conflito de agenda impedido |
| CT-008 | RF-008 | Validar simulador ROI (aluguel x instalação) | 1) Informar consumo/tarifa/investimento 2) Calcular | Mostra economia e tempo de retorno; gráfico gerado; valida entradas |
| CT-009 | RF-009 | Validar atualização de perfil e área | 1) Editar perfil 2) Desenhar/editar área no mapa 3) Salvar | Dados visíveis; versão auditável; mapa atualizado |
| CT-010 | RF-010 | Validar cadastro de empresa | 1) Preencher formulário e enviar documentos 2) Salvar | Validações OK; empresa criada com status 'Em verificação' |
| CT-011 | RF-011 | Validar recuperação de senha (cliente) | 1) Solicitar reset 2) Abrir link 3) Redefinir senha | Token single-use; expiração válida; login com nova senha |
| CT-012 | RF-012 | Validar notificações de status | 1) Alterar status de solicitações 2) Revisar preferências de notificação | Notificações enviadas segundo opt-in; histórico de envios |
| CT-013 | RF-013 | Validar opt-in/out de notificações | 1) Alterar preferências 2) Disparar evento | Sistema respeita preferências; logs registrados |
| CT-014 | RF-014 | Validar relatório mensal de desempenho | 1) Selecionar mês 2) Abrir relatório | KPIs corretos (leads, convertidos, taxa); exportação se existir |
| CT-015 | RF-015 | Validar gerenciamento de leads (CPL) | 1) Criar lead 2) Atribuir a empresa 3) Alterar status | Lead visível no painel; permissões respeitadas |
| CT-016 | RF-016 | Validar gerenciamento de planos e usuários | 1) Criar plano 2) Suspender e restaurar usuário | Regras aplicadas; notificações disparadas |
| CT-017 | RF-018 | Validar envio de proposta detalhada | 1) Preencher formulário completo (anexos?) 2) Enviar | Proposta salva; anexos validados; cliente pode visualizar/baixar |
| CT-018 | RF-019 | Validar comparação com dados inválidos | 1) Incluir proposta com valor negativo 2) Tentar comparar | Erro validado; comparação não é executada |
